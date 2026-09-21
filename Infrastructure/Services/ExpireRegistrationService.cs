using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyEventApi.Core.Enums;
using MyEventApi.Infrastructure.Data;

namespace MyEventApi.Infrastructure.Services
{
    public class ExpireRegistrationsService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpireRegistrationsService> _logger;

        public ExpireRegistrationsService(
            IServiceScopeFactory scopeFactory,
            ILogger<ExpireRegistrationsService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ExpireRegistrationsService iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await ExpireOldRegistrations();
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task ExpireOldRegistrations()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var expiredTime = DateTime.UtcNow.AddMinutes(-15);

                var expiredRegistrations = await context.Registrations
                    .Where(r =>
                        r.Status == ERegistrationStatus.Pending &&
                        r.CreatedAt < expiredTime)
                    .ToListAsync();

                if (expiredRegistrations.Any())
                {
                    foreach (var registration in expiredRegistrations)
                    {
                        registration.Status = ERegistrationStatus.Cancelled;
                    }

                    await context.SaveChangesAsync();

                    _logger.LogInformation(
                        "{Count} inscrições expiradas canceladas.",
                        expiredRegistrations.Count
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao expirar inscrições.");
            }
        }
    }
}