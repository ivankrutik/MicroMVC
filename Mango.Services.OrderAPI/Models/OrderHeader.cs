﻿namespace Mango.Services.OrderAPI.Models
{
    public class OrderHeader
    {
        public long? OrderHeaderId { get; set; }

        public string? UserId { get; set; }

        public string? CouponCode { get; set; }

        public decimal? OrderTotal { get; set; }

        public decimal? DiscountTotal { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? EMail { get; set; }

        public string? CardNumber { get; set; }

        public string? CVV { get; set; }

        public string? ExpiryMonthYear { get; set; }

        public DateTime? PickupDateTime { get; set; }
        public DateTime? OrderTime { get; set; }

        public int? CartTotalItems { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }

        public Boolean PaymentStatus { get; set; }
    }
}
