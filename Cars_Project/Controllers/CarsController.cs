 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Cars_Project.Models;
using Cars_Project.ViewModel;

namespace Cars_Project.Controllers
{
    [EnableCors("*","*","*")]
    public class CarsController : ApiController
    {
        private CarDbContext db = new CarDbContext();

        // GET: api/Cars
        public IQueryable<Car> GetCars()
        {
            return db.Cars;
        }

        // GET: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult GetCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.Id)
            {
                return BadRequest();
            }

            db.Entry(car).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //Custom put method for car 

        [HttpPut,Route("api/Edit/{id}")]
        public IHttpActionResult Edit(int id, CarVM c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c.Id)
            {
                return BadRequest();
            }
            
            var car = db.Cars.First(x => x.Id == id);
            car.Company = c.Company;
            car.Model = c.Model;
            car.ReleaseDate = c.ReleaseDate;
            car.Price = c.Price;

            if (!string.IsNullOrEmpty(c.Picture))
            {
                string fileName = "";
                if (c.Imagetype == "image/jpeg") fileName = Guid.NewGuid() + ".jpg";
                if (c.Imagetype == "image/jpg") fileName = Guid.NewGuid() + ".jpg";
                if (c.Imagetype == "image/png") fileName = Guid.NewGuid() + ".png";
                if (c.Imagetype == "image/gif") fileName = Guid.NewGuid() + ".gif";

                byte[] bytes = Convert.FromBase64String(c.Picture);

                File.WriteAllBytes(Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), fileName), bytes);
                car.Picture = fileName;
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(car);
        }
        // POST: api/Cars
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cars.Add(car);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = car.Id }, car);
        }

        //Custom post method for car 

        [HttpPost,Route("api/Create")]
        public IHttpActionResult CreateCar(CarVM c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Car car = new Car
            {
                Company=c.Company,
                Model=c.Model,
                ReleaseDate=c.ReleaseDate,
                Price=c.Price,
                Picture="blank.jpg"
            };
            string filename = "";
            if (c.Imagetype == "image/jpeg") filename = Guid.NewGuid() + ".jpg";
            if (c.Imagetype == "image/jpg") filename = Guid.NewGuid() + ".jpg";
            if (c.Imagetype == "image/png") filename = Guid.NewGuid() + ".png";
            if (c.Imagetype == "image/gif") filename = Guid.NewGuid() + ".gif";

            byte[] bytes = Convert.FromBase64String(c.Picture);
            File.WriteAllBytes(Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/") + filename), bytes);
            car.Picture = filename;
            db.Cars.Add(car);
            db.SaveChanges();

            return Ok(car);
        }
        // DELETE: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.Id == id) > 0;
        }
    }
}