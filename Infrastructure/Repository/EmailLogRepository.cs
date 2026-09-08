using MyEventApi.Core.Entity;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Data;
using Resend;

namespace MyEventApi.Infrastructure.Repository
{
    public class EmailLogRepository : IEmailLogRepository
    {
        private readonly AppDbContext _context;

        public EmailLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveLogEmail(EmailLogEntity emailLog)
        {
            await _context.EmailLogs.AddAsync(emailLog);
            await _context.SaveChangesAsync();
        }
    }
}
