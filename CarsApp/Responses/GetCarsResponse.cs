using System.Collections.Generic;
using CarsApp.Models;

namespace CarsApp.Responses
{
    public class GetCarsResponse : ResponseBase
    {
        /// <summary>
        /// List of found cars
        /// </summary>
        public List<Car> Item { get; set; }
    }
}