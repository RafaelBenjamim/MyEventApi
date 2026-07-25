using MyEventApi.Core.Dtos;

namespace MyEventApi.Core.Interfaces
{
    public interface IStoreService
    {
        Task UpdatePaymentSettings(UpdatePaymentSettingsRequest request);
    }
}
