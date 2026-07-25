namespace MyEventApi.Core.Dtos
{
    public class EventResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int MaxAttendees { get; set; }
        public int RegisteredCount { get; set; }
        public string? Location { get; set; }    
        public string? ImageUrl { get; set; }    
    }
}
