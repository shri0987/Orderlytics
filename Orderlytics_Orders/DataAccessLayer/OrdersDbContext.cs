using BusinessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FoodItemOrder> FoodItemOrders { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodItemOrder>()
                .HasKey(fo => new { fo.FoodId, fo.OrderId });

            modelBuilder.Entity<FoodItemOrder>()
                .HasOne<FoodItem>()
                .WithMany()
                .HasForeignKey(fo => fo.FoodId);

            modelBuilder.Entity<FoodItemOrder>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(fo => fo.OrderId);

        }

    }
}
