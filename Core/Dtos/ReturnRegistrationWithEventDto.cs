using MyEventApi.Core.Enums;

namespace MyEventApi.Core.Dtos
{
    public class ReturnRegistrationWithEventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string? Name { get; set; }
        public ERegistrationStatus Status { get; set; }
    }
}
