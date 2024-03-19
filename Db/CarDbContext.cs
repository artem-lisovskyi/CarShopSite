using CarShopCourseWork.Model.Data;
using CarShopCourseWork.Model.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarShopCourseWork.Db;

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Make> Makes { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entry =>
        {
            entry.ToTable("car");

            entry.HasOne(car => car.Make)
                .WithMany(make => make.Cars)
                .HasForeignKey(car => car.MakeId);
        });
        
        modelBuilder.Entity<Make>(entry =>
        {
            entry.ToTable("make");
        });
        
        modelBuilder.Entity<ShoppingCartItem>(entry =>
        {
            entry.ToTable("shopping_cart_item");
            
            entry.HasOne(item => item.Car)
                .WithMany()
                .HasForeignKey(item => item.CarId);
        });
    }
}