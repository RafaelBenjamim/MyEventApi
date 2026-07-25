using Microsoft.EntityFrameworkCore;
using MyEventApi.Core.Entities;
using MyEventApi.Core.Enums;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Data;

namespace MyEventApi.Infrastructure.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _context;

        public RegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountActiveByEvent(Guid eventId)
        {
            return await _context.Registrations.Where(r => r.EventId == eventId && r.Status != ERegistrationStatus.Cancelled).CountAsync();
        }

        public async Task addRegistration(RegistrationEntity registration)
        {
            await _context.Registrations.AddAsync(registration);
            await _context.SaveChangesAsync();  
        }

        public async Task UpdateRegistration(RegistrationEntity registration)
        {
            _context.Registrations.Update(registration);
            await _context.SaveChangesAsync();
        }

        public async Task<RegistrationEntity?> GetById(Guid id)
        {
            return await _context.Registrations.FirstOrDefaultAsync(r => r.Id == id);
        }

    }
}
