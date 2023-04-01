namespace Mango.Web.Models.CartDomain
{
    public class CartHeaderDto
    {
        public long CartHeaderId { get; set; }

        public string UserId { get; set; }

        public string CouponCode { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
