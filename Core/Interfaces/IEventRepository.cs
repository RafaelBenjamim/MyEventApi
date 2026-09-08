using MyEventApi.Core.Entities;

namespace MyEventApi.Core.Interfaces
{
    public interface IEventRepository
    {
        Task CreateEvent(EventEntity eventEntity);
        Task<EventEntity> GetCurrentByStore(Guid storeId);
        Task<EventEntity> GetCurrentBySlug(string storeSlug);
        Task<EventEntity> GetById(Guid eventId);
        Task<IEnumerable<EventEntity>> GetAllEventsByStore(string storeSlug);
        Task UpdateEvent(EventEntity eventEntity);
        Task DeleteEvent(Guid eventId);
    }

}
