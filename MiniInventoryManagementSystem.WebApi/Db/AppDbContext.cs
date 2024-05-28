 
using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.WebApi.Model;
using System.Security.Cryptography.X509Certificates;


namespace MiniInventoryManagementSystem.WebApi.Db
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
    }
}

