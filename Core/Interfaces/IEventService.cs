using MyEventApi.Core.Dtos;

namespace MyEventApi.Core.Interfaces
{
    public interface IEventService
    {
        Task<EventResponseDto> CreateEvent(CreateEventRequestDto request);
        Task<EventResponseDto> GetCurrentByStoreSlug(string storeSlug);
        Task<IEnumerable<EventResponseDto>> GetAllEventsByStore(string storeSlug);
        Task UpdateEvent(Guid eventId, UpdateEventRequestDto request);
        Task DeleteEvent(Guid eventId);
        Task<EventResponseDto> GetEventById(Guid eventId);

    }
}
