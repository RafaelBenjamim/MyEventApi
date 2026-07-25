using MyEventApi.Core.Entities;
using MyEventApi.Core.Enums;

namespace MyEventApi.Core.Dtos
{
    public class RegistrationResponseDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ERegistrationStatus Status { get; set; }
        public string PaymentUrl { get; set; } = string.Empty;
    }
}
