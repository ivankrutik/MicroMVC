using System.ComponentModel.DataAnnotations;

namespace Mango.Servises.CouponAPI.Model
{
    public class Coupon
    {
        [Key]
        public long CouponId { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponAmount { get; set; }
    }
}
