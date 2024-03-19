using CarShopCourseWork.Model.Data;

namespace CarShopCourseWork.Model.ViewModel
{
    public class CarListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public string CurrentMake { get; set; }
    }
}
