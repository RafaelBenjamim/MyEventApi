using MyEventApi.Core.Entities;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Data;

namespace MyEventApi.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _appDbContext;

        public PaymentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddPayment(PaymentEntity payment)
        {
            await _appDbContext.Payments.AddAsync(payment);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
