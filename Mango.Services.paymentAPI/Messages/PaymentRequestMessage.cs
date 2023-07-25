namespace Mango.Services.PaymentAPI.Messages
{
    public class PaymentRequestMessage
    {
        public long? OrderId { get; set; }
        public string? Name { get; set; }
        public string? CardNumber { get; set; }
        public string? CVV { get; set; }
        public string? ExpiryMonthYear { get; set; }
        public decimal? OrderTotal { get; set; }
    }
}
