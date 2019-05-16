using System.Collections.Generic;
using System.Linq;
using CarsFunctionApp.Entities;

namespace CarsFunctionApp
{
    public static class ExtensionMethods
    {
        public static List<Car> ByFilters(this List<Car> cars, Filters filters)
        {
            return filters == null ? cars : cars.ByState(filters.State).ByBrand(filters.Brand).ByModel(filters.Model)
                .ByYearFrom(filters.YearFrom).ByYearTo(filters.YearTo).ByPriceFrom(filters.PriceFrom)
                .ByPriceTo(filters.PriceTo);
        }
        public static List<Car> ByState(this List<Car> cars, string state)
        {
            if (string.IsNullOrEmpty(state)) return cars;
            var whereIsNew = state == "new";
            return cars.Where(x => x.IsNew == whereIsNew).ToList();
        }

        public static List<Car> ByBrand(this List<Car> cars, string brand)
        {
            return string.IsNullOrEmpty(brand) ? cars : cars.Where(x => x.Brand == brand).ToList();
        }

        public static List<Car> ByModel(this List<Car> cars, string model)
        {
            return string.IsNullOrEmpty(model) ? cars : cars.Where(x => x.Model == model).ToList();
        }
        public static List<Car> ByYearFrom(this List<Car> cars, int? yearFrom)
        {

            return yearFrom == null ? cars : cars.Where(x => x.Year >= yearFrom.Value).ToList();
        }

        public static List<Car> ByYearTo(this List<Car> cars, int? yearTo)
        {
            return yearTo == null ? cars : cars.Where(x => x.Year <= yearTo.Value).ToList();
        }

        public static List<Car> ByPriceFrom(this List<Car> cars, float? priceFrom)
        {
            return priceFrom == null ? cars : cars.Where(x => x.Price >= priceFrom.Value).ToList();
        }

        public static List<Car> ByPriceTo(this List<Car> cars, float? priceTo)
        {
            return priceTo == null ? cars : cars.Where(x => x.Price <= priceTo.Value).ToList();
        }
    }
}
