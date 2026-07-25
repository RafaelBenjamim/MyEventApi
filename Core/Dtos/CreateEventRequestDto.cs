namespace MyEventApi.Core.Dtos
{
    public class CreateEventRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int MaxAttendees { get; set; }
    }
}
