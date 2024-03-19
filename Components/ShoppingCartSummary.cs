using CarShopCourseWork.Model.Data;
using CarShopCourseWork.Model.Repository;
using CarShopCourseWork.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CarShopCourseWork.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }
    }
}
