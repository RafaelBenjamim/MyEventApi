using MyEventApi.Core.Dtos;
using MyEventApi.Core.Entities;
using MyEventApi.Core.Enums;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Repository;
using System.Diagnostics;

namespace MyEventApi.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEmailService _emailService;
        private readonly IEventRepository _eventRepository;

        public PaymentService(
            IRegistrationRepository registrationRepository,
            IPaymentRepository paymentRepository,
            IEmailService emailService,
            IEventRepository eventRepository)
        {
            _registrationRepository = registrationRepository;
            _paymentRepository = paymentRepository;
            _emailService = emailService;
            _eventRepository = eventRepository;
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

            var eventDetails = await _eventRepository.GetById(registration.EventId);

            if(eventDetails is not null)
            {
                var FormatedDate = eventDetails.Date.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("pt-BR"));

                await _emailService.SendPaymentConfirmation(
                email: registration.Email,
                name: registration.Name,
                eventTitle: eventDetails.Title,
                eventDate: FormatedDate,
                eventLocation: eventDetails.Location ?? "A confirmar",
                registrationId: registration.Id.ToString(),
                eventPrice: eventDetails.Price
            );
            }

        }
    }
}