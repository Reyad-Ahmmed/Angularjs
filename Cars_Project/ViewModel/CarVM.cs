using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cars_Project.ViewModel
{
    public class CarVM
    {
        public int Id { get; set; }
        [Required, StringLength(40)]
        public string Company { get; set; }
        [Required, StringLength(40)]
        public string Model { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
       
        public string Picture { get; set; }
        [StringLength(200)]
        public string Imagetype { get; set; }

    }
}