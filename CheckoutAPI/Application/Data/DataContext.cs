using Application.Entities;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Application.Data
{
    public class DataContext : DbContext
    {

        public DataContext()
        {

            Database.EnsureCreated();
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.EnableSensitiveDataLogging(true);
                options.UseSqlite(Environment.GetEnvironmentVariable(Constants.ConnectionStringKey));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Article>().HasData(
                new Article { Id = 1, ProductCategory = ProductCategory.Food, Description = "tomatoes", Price = 10 },
                new Article { Id = 2, ProductCategory = ProductCategory.Food, Description = "apples", Price = 6 },
                new Article { Id = 3, ProductCategory = ProductCategory.Tools, Description = "hammer", Price = 8 },
                new Article { Id = 4, ProductCategory = ProductCategory.Toys, Description = "lego", Price = 20 }
                );
        }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<BasketArticle> BasketArticles { get; set; }
    }
}
