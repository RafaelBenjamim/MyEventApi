using MyEventApi.Core.Enums;
using MyEventApi.Core.Interfaces;

namespace MyEventApi.Infrastructure.Services
{
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        private readonly PagBankGateway _pagBank;
        private readonly InfinitePayPaymentGateway _infinitePay;

        public PaymentGatewayFactory(PagBankGateway pagBank, InfinitePayPaymentGateway infinitePay)
        {
            _pagBank = pagBank;
            _infinitePay = infinitePay;
        }

        public IPaymentGateway GetGateway(EPaymentProvider provider)
        {
            return provider switch
            {
                EPaymentProvider.pagBank => _pagBank,
                EPaymentProvider.infinitePay => _infinitePay,
                _ => throw new InvalidOperationException($"Provedor de pagamento não suportado: {provider}")
            };
        }
    }
}
