using api_with_mongodb.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;


namespace mongo_db_demo.Data
{
    public class MongoContext : DbContext
    {
        public MongoContext(DbContextOptions<MongoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToCollection("products");
        }

        public DbSet<Product> Products { get; set; }
    }
}