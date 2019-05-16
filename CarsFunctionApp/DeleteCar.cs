using System.IO;
using System.Threading.Tasks;
using CarsFunctionApp.Entities;
using CarsFunctionApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CarsFunctionApp
{
    public static class DeleteCar
    {
        [FunctionName("DeleteCar")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var car = JsonConvert.DeserializeObject<Car>(requestBody);

            try
            {
                new CarService().DeleteCar(car);
                return new OkObjectResult(new BasicResponse { Success = true });
            }
            catch
            {
                return new OkObjectResult(new BasicResponse { Success = false });
            }
        }
    }
}
