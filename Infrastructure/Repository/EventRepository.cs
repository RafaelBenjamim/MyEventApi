using MyEventApi.Core.Entities;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MyEventApi.Infrastructure.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task CreateEvent(EventEntity eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }
       

        public async Task<EventEntity> GetCurrentByStore(Guid storeId)
        {
           return await _context.Events.Where(e => e.StoreId == storeId).OrderByDescending(e => e.Date).FirstOrDefaultAsync();
        }

        public async Task<EventEntity> GetCurrentBySlug(string storeSlug)
        {
            return await _context.Events.Include(e => e.Registrations).Where(e => e.Store.Slug == storeSlug).OrderByDescending(e => e.Date).FirstOrDefaultAsync();
        }

        public async Task<EventEntity> GetById(Guid id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<IEnumerable<EventEntity>> GetAllEventsByStore(string storeSlug)
        {
            var now = DateTime.UtcNow;

            return _context.Events
            .Include(e => e.Registrations)
            .Where(e => e.Store.Slug == storeSlug && e.Date >= now)
            .OrderBy(e => e.Date)
            .ToListAsync()
            .ContinueWith(t => (IEnumerable<EventEntity>)t.Result);
        }
    }
}
