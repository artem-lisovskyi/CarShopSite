using CarShopCourseWork.Model.Data;

namespace CarShopCourseWork.Model.interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> Cars { get; }
        IEnumerable<Car> PreferredCars { get; }
        Car GetCarById(int carId);
    }
}
