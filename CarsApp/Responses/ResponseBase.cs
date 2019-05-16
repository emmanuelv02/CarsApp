using System.Collections.Generic;

namespace CarsApp.Responses
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }

        public List<ValidationError> ValidationErrors { get; set; }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
