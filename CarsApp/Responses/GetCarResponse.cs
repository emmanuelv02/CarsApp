using CarsApp.Models;

namespace CarsApp.Responses
{
    public class GetCarResponse : ResponseBase
    {
        /// <summary>
        /// Found car
        /// </summary>
        public Car Item { get; set; }
    }
}