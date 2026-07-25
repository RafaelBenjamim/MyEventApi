namespace MyEventApi.Core.Entities
{
    public class PaymentEntity
    {
        public Guid Id { get; set; }
        public Guid RegistrationId { get; set; }
        public decimal AmountPaid { get; set; }
        public string GatewayStatus { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public DateTime? ConfirmedAt { get; set; }

        public RegistrationEntity Registration { get; set; } = null!;
    }
}
