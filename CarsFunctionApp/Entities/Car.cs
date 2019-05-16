using System.Collections.Generic;

namespace CarsFunctionApp.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public bool IsNew { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }

        public List<string> Photos { get; set; }
    }
}
