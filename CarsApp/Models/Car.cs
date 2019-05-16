using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarsApp.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "State")]
        public bool IsNew { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(typeof(int),"1970","2020")]
        public int Year { get; set; }

        [Required]
        public float Price { get; set; }

        public List<string> Photos { get; set; }
    }
}
