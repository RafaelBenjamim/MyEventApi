using Microsoft.EntityFrameworkCore;
using MyEventApi.Core.Entities;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Data;

namespace MyEventApi.Infrastructure.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;

        public StoreRepository(AppDbContext context) => _context = context;

        public async Task<bool> SlugExists(string slug){
            return _context.Stores.Any(s => s.Slug == slug);
        }

        public async Task AddStore(StoreEntity store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task<StoreEntity> GetByIdStore(Guid id)
        {
            return await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateStore(StoreEntity store)
        {
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
        }
    }
}
