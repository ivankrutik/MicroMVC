using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public long CartHeaderId { get; set; }

        public long UserId { get; set; }

        public string CouponCode { get; set; }
    }
}
