using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarsApp.Models;
using CarsApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarsApp.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult Index()
        {
            return View("Search");
        }

        // GET: Cars/Details/5
        public ActionResult Details(int id)
        {
            //Get car
            var car = ApiCallsService.GetCarRequest(id).Item;
            if (car == null) return NotFound();
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(Filters filters)
        {
            try
            {
                if (filters.State == "any") filters.State = null;

                var cars = ApiCallsService.GetCarsRequest(filters);

                //Save filter into the session
                HttpContext.Session.SetString("filters", JsonConvert.SerializeObject(filters));
                return View(cars.Item);
            }
            catch
            {
                return View();
            }
        }

        // GET: Cars/Edit/5
        public ActionResult List()
        {
            //Get filter from session
            var sFilters = HttpContext.Session.GetString("filters");
            var filters = sFilters == null ? new Filters() : JsonConvert.DeserializeObject<Filters>(sFilters);

            if (filters.State == "any") filters.State = null;
            var cars = ApiCallsService.GetCarsRequest(filters);

            return View(cars.Item);
        }


        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            try
            {
                ApiCallsService.CreateCarRequest(car);

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int id)
        {
            //Get car
            var car = ApiCallsService.GetCarRequest(id).Item;
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            try
            {
                var response = ApiCallsService.UpdateCarRequest(car);
                //Some error
                if (response == null || !response.Success) return NotFound("Could not edit");

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cars/Edit/5
        public ActionResult Delete(int id)
        {
            //Get car
            var car = ApiCallsService.GetCarRequest(id).Item;
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Car car)
        {
            try
            {
                var response = ApiCallsService.DeleteCarRequest(car);
                //Some error
                if (response == null || !response.Success) return NotFound("Could not delete");

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return NotFound("Error");
            }
        }

        [HttpPost]
        public ActionResult GetAllBrands(string term)
        {
            try
            {
                var result = ApiCallsService.GetAllBrandsRequest().Item
                    .Where(x => x.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));

                return new JsonResult(result);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetModels(string term, string brand)
        {
            try
            {
                var req = Request;

                var result = ApiCallsService.GetModelsRequest(brand).Item
                    .Where(x => x.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));

                return new JsonResult(result);
            }
            catch
            {
                return View();
            }
        }
    }
}