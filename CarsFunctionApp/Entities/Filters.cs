namespace CarsFunctionApp.Entities
{
    public class Filters
    {
        public string State { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public float? PriceFrom { get; set; }
        public float? PriceTo { get; set; }
    }
}