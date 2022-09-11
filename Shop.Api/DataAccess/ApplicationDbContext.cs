using Microsoft.EntityFrameworkCore;
using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Models.DbModels;

namespace Shop.Api.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
    }
}
