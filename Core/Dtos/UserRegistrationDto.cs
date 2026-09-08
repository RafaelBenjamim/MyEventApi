using MyEventApi.Core.Enums;

namespace MyEventApi.Core.Dtos
{
    public class UserRegistrationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ERegistrationStatus status { get; set; }
    }
}
