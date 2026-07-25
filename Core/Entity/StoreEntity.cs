using Microsoft.Extensions.Logging;
using MyEventApi.Core.Enums;

namespace MyEventApi.Core.Entities
{
    public class StoreEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? WhatsApp { get; set; }
        public string? Instagram { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public EPaymentProvider? PaymentProvider { get; set; }
        public string? PagBankAppKey { get; set; }
        public string? PagBankAppId { get; set; }
        public string? InfinitePayHandle { get; set; }

        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
        public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
    }
}
