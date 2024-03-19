using CarShopCourseWork.Model.Data;

namespace CarShopCourseWork.Model.interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
