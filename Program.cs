using CarShopCourseWork.Db;
using CarShopCourseWork.Model.data;
using CarShopCourseWork.Model.Data;
using CarShopCourseWork.Model.interfaces;
using CarShopCourseWork.Model.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var services = builder.Services;

services.AddTransient<IMakeRepository, DataMakeRepository>();
services.AddTransient<ICarRepository, DataCarRepository>();

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddScoped(sp => ShoppingCart.GetCart(sp, "Model/DataStore/ShoppingCart.json"));
services.AddTransient<List<Order>>();
services.AddTransient<List<OrderDetail>>();
services.AddTransient<IOrderRepository, DataOrderRepository>();

services.AddMvc();
services.AddSession();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
services.AddDbContext<CarDbContext>(options => options.UseMySql(connectionString, serverVersion));

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.MapControllerRoute(
    name: "countryfilter",
    pattern: "Car/{action}/{make?}",
    defaults: new { Controller = "Car", action = "List" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{Id?}");
app.Run();
