using MyEventApi.Core.Enums;

namespace MyEventApi.Core.Interfaces
{
    public interface IPaymentGatewayFactory
    {
        IPaymentGateway GetGateway(EPaymentProvider provider);
    }
}
