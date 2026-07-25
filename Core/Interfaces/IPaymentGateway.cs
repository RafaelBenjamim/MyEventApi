using MyEventApi.Core.Entities;
using MyEventApi.Core.Results;

namespace MyEventApi.Core.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentChargeResult> CreateCharge (StoreEntity sotre, decimal amount, string description, Guid orderId, string customerName, string customerEmail, string customerPhone);
    }
}
