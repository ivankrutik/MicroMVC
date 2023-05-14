using Mango.Servises.CouponAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Mango.Servises.CouponAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    CouponId = 1,
                    CouponCode = "10OFF",
                    CouponAmount = 10
                });
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    CouponId = 2,
                    CouponCode = "20OFF",
                    CouponAmount = 20
                });
        }
    }
}
