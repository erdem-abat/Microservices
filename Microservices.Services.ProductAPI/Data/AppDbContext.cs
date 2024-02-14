using Microservices.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Keyboard",
                Price = 10,
                Description = "Keyboard description",
                ImageUrl = "https://m.media-amazon.com/images/I/61jPovnNr-L._AC_SL1200_.jpg",
                CategoryName = "Computer"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Mouse",
                Price = 10,
                Description = "Mouse description",
                ImageUrl = "https://productimages.hepsiburada.net/s/468/550/110000506050793.jpg/format:webp",
                CategoryName = "Computer"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Motherboard",
                Price = 10,
                Description = "Motherboard description",
                ImageUrl = "https://dlcdnwebimgs.asus.com/gain/fdae2676-06f0-41e4-a184-8547aedb3b9c/w692",
                CategoryName = "Computer"
            });

        }
    }
}