using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cars_Project.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required,StringLength(40)]
        public string Company { get; set; }
        [Required, StringLength(40)]
        public string Model { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, Column(TypeName ="date"),DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required, StringLength(200)]
        public string Picture { get; set; }
    }
    public class CarDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}