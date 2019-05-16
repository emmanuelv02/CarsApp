using System.Collections.Generic;

namespace CarsApp.Responses
{
    public class GetAllBrandsResponse: ResponseBase
    {
        /// <summary>
        /// List of found brands
        /// </summary>
        public List<string> Item { get; set; }
    }
}