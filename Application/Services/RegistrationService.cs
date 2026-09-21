using MyEventApi.Core.Dtos;
using MyEventApi.Core.Entities;
using MyEventApi.Core.Enums;
using MyEventApi.Core.Interfaces;

namespace MyEventApi.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IPaymentGatewayFactory _gatewayFactory;
        private readonly IStoreRepository _storeRepository;


        public RegistrationService (IRegistrationRepository registrationRepository, IEventRepository eventRepository, IPaymentGatewayFactory gatewayFactory, IStoreRepository storeRepository)
        {
            _registrationRepository = registrationRepository;
            _eventRepository = eventRepository;
            _gatewayFactory = gatewayFactory;
            _storeRepository = storeRepository;
        }


        public async Task<RegistrationResponseDto> CreateRegistration(CreateRegistrationRequest request)
        {
            var eventEntity = await _eventRepository.GetById(request.EventId);

            if (eventEntity == null)
                throw new InvalidOperationException("Evento não encontrado!");

            var activeCount = await _registrationRepository.CountActiveByEvent(request.EventId);

            if(activeCount >= eventEntity.MaxAttendees)
                throw new InvalidOperationException("Vagas esgotadas para este evento!");

            var store = await _storeRepository.GetByIdStore(eventEntity.StoreId);
            if (store is null)
                throw new InvalidOperationException("Loja não encontrada!");

            if (store.PaymentProvider is null)
                throw new InvalidOperationException("Esta loja ainda não configurou um método de pagamento.");

             

            var hasDiscount = false;
            var finalPrice = eventEntity.Price;

            if (eventEntity.DiscountPercentage !=  null )
            {
                var returnCustomer = await _registrationRepository.IsMemberFiorella(request.Email);

                if (returnCustomer != null) 
                {
                    hasDiscount = true;
                    var discount = eventEntity.Price * (eventEntity.DiscountPercentage / 100);
                    finalPrice = eventEntity.Price - discount;
                }
            }

            var registration = new RegistrationEntity
            {
                Id = Guid.NewGuid(),
                EventId = request.EventId,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Status = ERegistrationStatus.Pending,
                HasDiscount = hasDiscount,
                DiscountPercentage = hasDiscount ? eventEntity.DiscountPercentage : 0,
                FinalPrice = finalPrice
            };
            await _registrationRepository.addRegistration(registration);

            var gateway = _gatewayFactory.GetGateway(store.PaymentProvider.Value);
            var charge = await gateway.CreateCharge(store, finalPrice, eventEntity.Title, registration.Id, request.Name, request.Email, request.Phone);

            return new RegistrationResponseDto
            {
                Id = registration.Id,
                EventId = registration.EventId,
                Name = registration.Name,
                Status = registration.Status,
                PaymentUrl = charge.PaymentUrl
            };
        }
        public async Task<ReturnRegistrationWithEventDto> GetConfirmationEvent(Guid id)
        {
            var registration = await _registrationRepository.GetbyIdWithEvent(id);

            if (registration == null)
                throw new InvalidOperationException("Inscrição não encontrada!");

            return new ReturnRegistrationWithEventDto
            {
                Id = registration.Id,
                Name = registration.Name,
                Title = registration.Event.Title,
                Date = registration.Event.Date,
                Location = registration.Event.Location,
                Status = registration.Status
            };
        }

        public async Task<List<UserRegistrationDto>> GetRegistrationsByEvent(Guid eventId)
        {
            var registration = await    _registrationRepository.GetRegistrationsByEvent(eventId);

            if(registration == null)
                throw new InvalidOperationException("Esse evento ainda não possui nenhuma inscrição!");
            
            return registration;
        }
    }
}
