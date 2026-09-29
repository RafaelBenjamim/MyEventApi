using MyEventApi.Core.Entities;
using MyEventApi.Core.Entity;
using MyEventApi.Core.Enums;
using MyEventApi.Infrastructure.Repository;

namespace MyEventApi.Core.Entities
{     

    public class RegistrationEntity
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ERegistrationStatus Status { get; set; } = ERegistrationStatus.Pending;
        public string? ExternalPaymentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool HasDiscount { get; set; } = false;
        public decimal DiscountPercentage { get; set; }
        public decimal FinalPrice { get; set; }
        public bool AgreedToTerms { get; set; }


        public EventEntity Event { get; set; } = null!;
        public PaymentEntity? Payment { get; set; }

        public ICollection<EmailLogEntity> EmailLogs { get; set; } = new List<EmailLogEntity>();
    }
}
