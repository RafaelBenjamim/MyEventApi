using MyEventApi.Core.Dtos;

namespace MyEventApi.Core.Interfaces
{
    public interface IPaymentService
    {
        Task HandleWebhook(PaymentWebhookRequestDto request);
    }
}
