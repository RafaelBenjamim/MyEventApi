using MyEventApi.Core.Entities;

namespace MyEventApi.Core.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddPayment(PaymentEntity payment);
    }
}
