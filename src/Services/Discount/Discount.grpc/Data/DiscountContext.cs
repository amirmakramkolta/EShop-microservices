using Discount.grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.grpc.Data
{
    public class DiscountContext : DbContext
    {
        protected DiscountContext()
        {
            
        }
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }
        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                [
                new Coupon() { Id = 1, ProductName="iPhone 17 pro max", Description = "iPhone discount", Amount = 10 },
                new Coupon() { Id = 2, ProductName="Samsung S25 Ultra", Description = "Samsung discount", Amount = 15 }
            ]);
        }
    }
}
