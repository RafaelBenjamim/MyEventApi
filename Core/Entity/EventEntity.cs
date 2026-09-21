namespace MyEventApi.Core.Entities
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int MaxAttendees { get; set; }
        public string? Location { get; set; }    
        public string? ImageUrl { get; set; }
        public decimal DiscountPercentage { get; set; }

        public StoreEntity Store { get; set; } = null!;
        public ICollection<RegistrationEntity> Registrations { get; set; } = new List<RegistrationEntity>();
    }
}
