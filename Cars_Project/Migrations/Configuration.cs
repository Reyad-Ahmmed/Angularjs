namespace Cars_Project.Migrations
{
    using Cars_Project.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cars_Project.Models.CarDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Cars_Project.Models.CarDbContext db)
        {
            var c = new Car
            {
                Company = "Tesla",
                Model = "Model X",
                ReleaseDate = DateTime.Parse("2021-02-10"),
                Price = 35000.00M,
                Picture = "blank.jpg"
            };
            db.Cars.Add(c);
            db.SaveChanges();
        }
    }
}
