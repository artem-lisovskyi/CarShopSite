using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using CarShopCourseWork.Db;
using CarShopCourseWork.Model.Data;
using Microsoft.EntityFrameworkCore;

namespace CarShopCourseWork.Model.Repository
{
    public class ShoppingCart
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _jsonFilePath;
        private readonly CarDbContext _dbContext;

        private ShoppingCart(IHttpContextAccessor httpContextAccessor, string jsonFilePath, CarDbContext carDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _jsonFilePath = jsonFilePath;
            _dbContext = carDbContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services, string jsonFilePath)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            var context = services.GetRequiredService<CarDbContext>();
            
            return new ShoppingCart(
                services.GetRequiredService<IHttpContextAccessor>(),
                jsonFilePath,
                context)
            {
                ShoppingCartId = cartId,
                ShoppingCartItems = context.ShoppingCartItems
                    .Include(c => c.Car)
                    .ThenInclude(c => c.Make)
                    .Where(c => c.ShoppingCartId == cartId).ToList()
            };
        }

        public void AddToCart(Car car, int amount)
        {
            var shoppingCartItem = ShoppingCartItems.SingleOrDefault(
                s => s.Car.CarId == car.CarId && s.ShoppingCartId == ShoppingCartId
            );

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Car = car,
                    Amount = 1
                };

                ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            SaveChanges();
        }

        public int RemoveFromCart(Car car)
        {
            var shoppingCartItem = ShoppingCartItems.SingleOrDefault(
                s => s.Car.CarId == car.CarId && s.ShoppingCartId == ShoppingCartId
            );

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems;
        }

        public void ClearCart()
        {
            ShoppingCartItems.Clear();
            SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            return ShoppingCartItems
                .Sum(c => c.Car.Price * c.Amount);
        }

        private void SaveChanges()
        {
            var existingCartItems = _dbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId);
            foreach (var existingCartItem in existingCartItems)
            {
                _dbContext.Remove(existingCartItem);
            }
            _dbContext.SaveChanges();
            
            foreach (var shoppingCartItem in ShoppingCartItems)
            {
                shoppingCartItem.Id = 0;
                _dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            
            _dbContext.SaveChanges();
        }
    }


}
