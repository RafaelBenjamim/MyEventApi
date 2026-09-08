using MyEventApi.Core.Entities;

namespace MyEventApi.Core.Entity
{
    public class EmailLogEntity
    {
        public Guid Id { get; set; }
        public Guid RegistrationId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;  
        public string? ErrorMessage { get; set; }
        public DateTime Sent { get; set; } = DateTime.UtcNow;

        public RegistrationEntity Registration { get; set; } = null!;
    }

}
