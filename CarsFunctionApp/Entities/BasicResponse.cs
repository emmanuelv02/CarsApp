using System.Collections.Generic;

namespace CarsFunctionApp.Entities
{
    public class BasicResponse
    {
        public bool Success { get; set; }
        public object Item { get; set; }

        public List<ValidationError> ValidationErrors { get; set; }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}