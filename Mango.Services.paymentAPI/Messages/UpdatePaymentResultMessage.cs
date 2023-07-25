namespace Mango.Services.PaymentAPI.Messages.Pay
{
    public class UpdatePaymentResultMessage
    {
        public long? OrderId { get; set; }
        public bool Status { get; set; }
    }
}
