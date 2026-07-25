using MyEventApi.Core.Entities;
using MyEventApi.Core.Interfaces;
using MyEventApi.Core.Results;

namespace MyEventApi.Infrastructure.Services
{
    public class PagBankGateway : IPaymentGateway
    {
        public Task<PaymentChargeResult> CreateCharge(StoreEntity sotre, decimal amount, string description, Guid orderId, string customerName, string customerEmail, string customerPhone)
        {
            throw new NotImplementedException("PagBank ainda não implementado.");
        }
    }
}
