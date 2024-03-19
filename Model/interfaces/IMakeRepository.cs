using CarShopCourseWork.Model.Data;

namespace CarShopCourseWork.Model.interfaces
{
    public interface IMakeRepository
    {
        IEnumerable<Make> Make { get; }
    }
}
