using Microsoft.EntityFrameworkCore;

namespace Category.Service.Context
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        //entities
        //public DbSet<Category> Categories { get; set; }
      
    }
}
