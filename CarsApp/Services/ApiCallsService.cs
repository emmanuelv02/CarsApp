using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using CarsApp.Models;
using CarsApp.Responses;
using Newtonsoft.Json;

namespace CarsApp.Services
{
    public static class ApiCallsService
    {
        //For this test I'm going to ignore validation errors
        private static HttpClient _client;

        private static HttpClient GetClient()
        {
            if (_client != null) return _client;
            // Local path "http://localhost:7071/api/"
            _client = new HttpClient { BaseAddress = new Uri("https://efuncapi.azurewebsites.net/api/") };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }

        //Get cars by filter
        public static GetCarsResponse GetCarsRequest(Filters filters)
        {
            return Post<GetCarsResponse, Filters>("GetCars", filters);
        }

        //Create car
        public static CreateCarResponse CreateCarRequest(Car car)
        {
            return Post<CreateCarResponse, Car>("CreateCar", car);
        }
        //Get all brands
        public static GetAllBrandsResponse GetAllBrandsRequest()
        {
            return Post<GetAllBrandsResponse, object>("GetAllBrands", new { });
        }
        //Get models by brand
        public static GetModelsResponse GetModelsRequest(string brand)
        {
            return Post<GetModelsResponse, string>("GetModels", brand);
        }
        //Get Car
        public static GetCarResponse GetCarRequest(int id)
        {
            return Post<GetCarResponse, int>("GetCar", id);
        }
        //Update Car
        public static UpdateCarResponse UpdateCarRequest(Car car)
        {
            return Post<UpdateCarResponse, Car>("UpdateCar", car);
        }
        //Delete car
        public static DeleteCarResponse DeleteCarRequest(Car car)
        {
            return Post<DeleteCarResponse, Car>("DeleteCar", car);
        }


        private static TResponse Post<TResponse, TRequest>(string requestUri, TRequest requestModel)
        {
            var client = GetClient();
            var jsonModel = JsonConvert.SerializeObject(requestModel);
            var httpContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");

            var response = client.PostAsync(requestUri, httpContent).Result;
            response.EnsureSuccessStatusCode();

            // If the response contains content
            if (response.Content != null)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TResponse>(responseContent);
            }
            else
            {
                throw new Exception("The api didn't return anything");
            }
        }
    }
}