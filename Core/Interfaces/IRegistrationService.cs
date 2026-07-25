using MyEventApi.Core.Dtos;

namespace MyEventApi.Core.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationResponseDto> CreateRegistration(CreateRegistrationRequest request);
    }
}
