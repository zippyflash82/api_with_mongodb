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
            modelBuilder.Entity<UserModel>().ToCollection("users");
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseMongoDB("mongodb://localhost:27017", "productsdb");
        //     }
        // }



        public DbSet<Product> Products { get; set; }

        public DbSet<UserModel> Users { get; set; }
    }
}