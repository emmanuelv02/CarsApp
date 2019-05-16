using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarsFunctionApp.Entities;
using CarsFunctionApp.Services;
using CarsFunctionApp.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CarsFunctionApp
{
    public static class CreateCar
    {
        [FunctionName("CreateCar")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var car = JsonConvert.DeserializeObject<Car>(requestBody);

            log.LogInformation($"Request to add car {car.Brand} {car.Model} : {car.Year} received.");
            var validationResult = new CarValidator().Validate(car);

            if (!validationResult.IsValid)
            {
                var modelStateD = new ModelStateDictionary();
                var validationErrors = validationResult.Errors.Select(error => new ValidationError { PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage }).ToList();
                return new BadRequestObjectResult(new BasicResponse { ValidationErrors = validationErrors });
            }

            var carService = new CarService();
            var id = carService.CreateCar(car);

            return new OkObjectResult(new BasicResponse { Success = true, Item = id });
        }
    }
}
