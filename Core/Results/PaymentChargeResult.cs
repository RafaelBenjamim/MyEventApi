namespace MyEventApi.Core.Results
{
    public class PaymentChargeResult
    {
        public string PaymentUrl { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
    }
}
