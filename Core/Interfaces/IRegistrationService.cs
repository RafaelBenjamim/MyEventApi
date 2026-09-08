using MyEventApi.Core.Dtos;

namespace MyEventApi.Core.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationResponseDto> CreateRegistration(CreateRegistrationRequest request);
        Task<ReturnRegistrationWithEventDto> GetConfirmationEvent(Guid id);
        Task<List<UserRegistrationDto>> GetRegistrationsByEvent(Guid eventId);
    }
}
