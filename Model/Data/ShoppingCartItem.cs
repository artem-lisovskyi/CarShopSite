using CarShopCourseWork.Model.Repository;

namespace CarShopCourseWork.Model.Data
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        
        public int CarId { get; set; }
        public Car Car { get; set; }
        
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
