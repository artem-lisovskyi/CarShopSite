using CarShopCourseWork.Model.Data;
using CarShopCourseWork.Model.interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using CarShopCourseWork.Db;
using Microsoft.EntityFrameworkCore;

namespace CarShopCourseWork.Model.data
{
    public class DataCarRepository : ICarRepository
    {
        private List<Car> _cars;
        private readonly CarDbContext _dbContext;

        public DataCarRepository(CarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Car> Cars
        {
            get
            {
                var cars = _dbContext.Cars.Include(c => c.Make).ToList();
                foreach (var car in cars)
                {
                    var sampleData = new CarClassificationModel.ModelInput()
                    {
                        Col0 = car.buying,
                        Col1 = car.maint,
                        Col2 = car.doors,
                        Col3 = car.persons,
                        Col4 = car.lug_boot,
                        Col5 = car.safety
                    };
                    car.CarClass = CarClassificationModel.Predict(sampleData).PredictedLabel;
                }

                return cars;
            }
        }
        public IEnumerable<Car> PreferredCars => Cars.Where(c => c.IsPreferredCar).ToList();
        public Car GetCarById(int carId) => Cars.FirstOrDefault(c => c.CarId == carId);
    }
}

