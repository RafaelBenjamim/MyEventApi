using MyEventApi.Core.Entities;

namespace MyEventApi.Core.Interfaces
{
    public interface IStoreRepository
    {
        Task<bool> SlugExists(string slug);
        Task AddStore(StoreEntity store);
        Task<StoreEntity?> GetByIdStore(Guid id);  
        Task UpdateStore(StoreEntity store);
    }
}
