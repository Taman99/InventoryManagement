using Microsoft.EntityFrameworkCore;
using Product.Service.Entities;

namespace Product.Service.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        //entities
//        public DbSet<Product> Products { get; set; }

    }
}
