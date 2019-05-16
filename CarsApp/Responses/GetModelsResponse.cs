using System.Collections.Generic;

namespace CarsApp.Responses
{
    public class GetModelsResponse : ResponseBase
    {
        /// <summary>
        /// List of found models
        /// </summary>
        public List<string> Item { get; set; }
    }
}