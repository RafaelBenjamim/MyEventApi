using MyEventApi.Core.Dtos;

namespace MyEventApi.Core.Interfaces
{
    public interface IEventService
    {
        Task<EventResponseDto> CreateEvent(CreateEventRequestDto request);

        Task<EventResponseDto> GetCurrentByStoreSlug(string storeSlug);

        Task<IEnumerable<EventResponseDto>> GetAllEventsByStore(string storeSlug);
    }
}
