using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication2.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication2.Data
{
    public  class SeedData
    {
        public async static  Task Initialize(IServiceProvider serviceProvider)
        {
            // Delete and recreate the database to start fresh
            EStoreContext context = new EStoreContext(serviceProvider.GetRequiredService<DbContextOptions<EStoreContext>>());
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            // Seed Brands
            Brand brand1 = new Brand { Name = "Acer" };
            Brand brand2 = new Brand { Name = "Apple" };
            Brand brand3 = new Brand { Name = "Lenovo" };
            if (!context.Brands.Any()) {

                context.Brands.AddRange(brand1, brand2, brand3);
                context.SaveChanges();
            }
            
            

            // Seed Stores
            Store store1 = new Store
            {
              
                StreetName = "Main Street",
                StreetNumber = "123",
                Province = Canadian.Ontario
            };
            Store store2 = new Store
            {
               
                StreetName = "Oak Avenue",
                StreetNumber = "456",
                Province = Canadian.Alberta
            };
            Store store3 = new Store
            {
               
                StreetName = "Maple Drive",
                StreetNumber = "789",
                Province = Canadian.BritishColumbia
            };

            if (!context.Store.Any()) {
                context.Store.AddRange(store1, store2, store3);
                context.SaveChanges();
            }
               

            // Seed Laptops
            Laptop laptop1 = new Laptop
            {
             
                Model = "Laptop Model X",
                Price = 1000,
                Condition = LaptopCondition.New,
                Brand = brand1
            };
            Laptop laptop2 = new Laptop
            {
               
                Model = "Laptop Model Y",
                Price = 800,
                Condition = LaptopCondition.Used,
                Brand = brand2
            };
            Laptop laptop3 = new Laptop
            {
               
                Model = "Laptop Model Z",
                Price = 1200,
                Condition = LaptopCondition.Refurbished,
                Brand = brand1
            };
            if (!context.Laptops.Any()) {
                context.Laptops.AddRange(laptop1, laptop2, laptop3);
                context.SaveChanges();
            }
            

            LaptopStore laptopStore1 = new LaptopStore
            {
                Id = Guid.NewGuid(),
                StoreNumber = store1.StoreNumber,
                LaptopsNumber = laptop1.LaptopsNumber,
                Quantity = 10

            };

            LaptopStore laptopStore2 = new LaptopStore
            {
                Id = Guid.NewGuid(),
                StoreNumber = store2.StoreNumber,
                LaptopsNumber = laptop3.LaptopsNumber,
                Quantity = 5
            };

            LaptopStore laptopStore3 = new LaptopStore
            {
                Id = Guid.NewGuid(),
                StoreNumber = store3.StoreNumber,
                LaptopsNumber = laptop3.LaptopsNumber,
                Quantity = 8
            };

            LaptopStore laptopStore4 = new LaptopStore
            {
                Id = Guid.NewGuid(),
                StoreNumber = store2.StoreNumber,
                LaptopsNumber = laptop1.LaptopsNumber,
                Quantity = 12
            };

            context.LaptopStore.AddRange(laptopStore1, laptopStore2, laptopStore3, laptopStore4);
            context.SaveChanges();

        }
    }
}








