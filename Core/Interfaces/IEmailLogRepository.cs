using MyEventApi.Core.Entity;

namespace MyEventApi.Core.Interfaces
{
    public interface IEmailLogRepository
    {
        Task SaveLogEmail(EmailLogEntity emailLog);
    }
}
