using MyEventApi.Core.Dtos;
using MyEventApi.Core.Entities;
using MyEventApi.Core.Enums;
using MyEventApi.Core.Interfaces;

namespace MyEventApi.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(
            IRegistrationRepository registrationRepository,
            IPaymentRepository paymentRepository)
        {
            _registrationRepository = registrationRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task HandleWebhook(PaymentWebhookRequestDto request)
        {
            if (!Guid.TryParse(request.OrderNsu, out var registrationId))
                throw new InvalidOperationException("OrderNsu inválido.");

            var registration = await _registrationRepository.GetById(registrationId);

            if (registration is null)
                throw new InvalidOperationException("Nenhuma inscrição encontrada.");

            if (registration.Status != ERegistrationStatus.Pending)
                return;

                registration.Status = ERegistrationStatus.Confirmed;
                await _registrationRepository.UpdateRegistration(registration);

                var payment = new PaymentEntity
                {
                    Id = Guid.NewGuid(),
                    RegistrationId = registration.Id,
                    AmountPaid = request.PaidAmount / 100m,  
                    GatewayStatus = request.Status,
                    TransactionId = request.TransactionNsu,
                    ConfirmedAt = DateTime.UtcNow
                };
                await _paymentRepository.AddPayment(payment);
        }
    }
}