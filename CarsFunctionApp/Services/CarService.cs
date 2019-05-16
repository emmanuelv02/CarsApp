using System.Collections.Generic;
using System.Linq;
using CarsFunctionApp.Entities;
using CarsFunctionApp.Repositories;

namespace CarsFunctionApp.Services
{
    public class CarService
    {
        public long CreateCar(Car car)
        {
            var carRepository = new CarRepository();
            var carId = carRepository.AddCar(car);

            if (car.Photos == null || !car.Photos.Any()) return carId;

            var photosRepository = new PhotosRepository();
            foreach (var imageFile in car.Photos)
            {
                photosRepository.AddPhoto(imageFile, carId);
            }

            return carId;
        }

        public List<Car> GetCars(Filters filters)
        {
            var cars = new CarRepository().GetAll().ByFilters(filters);

            if (!cars.Any()) return cars;

            var photosRepository = new PhotosRepository();
            foreach (var car in cars)
            {
                var photos = photosRepository.GetByCarId(car.Id);
                car.Photos = photos.Any() ? photos : null;
            }

            return cars;
        }

        public void DeleteCar(Car car)
        {
            new PhotosRepository().DeletePhotosByCarId(car.Id);
            new CarRepository().DeleteCar(car);
        }

        public Car GetById(int id)
        {
            var car = new CarRepository().GetById(id);
            if (car == null) return null;

            var photosRepository = new PhotosRepository();
            var photos = photosRepository.GetByCarId(car.Id);
            car.Photos = photos.Any() ? photos : null;

            return car;
        }
    }
}
