using CarShopCourseWork.Model.interfaces;
using CarShopCourseWork.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CarShopCourseWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;
        public HomeController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredCars = _carRepository.PreferredCars
            };
            return View(homeViewModel);
        }
    }
}
