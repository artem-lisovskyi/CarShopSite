using CarShopCourseWork.Model.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarShopCourseWork.Model.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Car> PreferredCars { get; set; }
    }
}
