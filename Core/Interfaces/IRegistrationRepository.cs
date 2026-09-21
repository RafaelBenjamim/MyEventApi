using MyEventApi.Core.Dtos;
using MyEventApi.Core.Entities;

namespace MyEventApi.Core.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<int> CountActiveByEvent(Guid eventId);
        Task addRegistration(RegistrationEntity registration);
        Task UpdateRegistration(RegistrationEntity registration);
        Task<RegistrationEntity?> GetById(Guid id);
        Task<RegistrationEntity> GetbyIdWithEvent(Guid id);
        Task<List<UserRegistrationDto>> GetRegistrationsByEvent(Guid Id);
        Task<bool> IsMemberFiorella(string email);
    }
}
