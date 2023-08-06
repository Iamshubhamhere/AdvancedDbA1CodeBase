using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class EStoreContext: DbContext
    {
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>().HasKey(s => s.StoreNumber);
            modelBuilder.Entity<Brand>().HasKey(b => b.BrandId);
            modelBuilder.Entity<Laptop>().HasKey(l => l.LaptopsNumber);
            modelBuilder.Entity<LaptopStore>().HasKey(l => new { l.Id, l.StoreNumber, l.LaptopsNumber });
            modelBuilder.Entity<LaptopStore>()
                .HasOne(l => l.Store)
                .WithMany(b => b.LaptopStores)
                .HasForeignKey(l => l.StoreNumber);
            modelBuilder.Entity<LaptopStore>()
                .HasOne(l => l.Laptops)
                .WithMany(b => b.LaptopStores)
                .HasForeignKey(l => l.LaptopsNumber);
                
        }
        public EStoreContext(DbContextOptions options) : base(options) { }


        public DbSet<Laptop> Laptops { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Store> Store { get; set; } = null!;
        public DbSet<LaptopStore> LaptopStore { get; set; } = null!;
    }
}
