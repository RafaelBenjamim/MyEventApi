using Microsoft.EntityFrameworkCore.Metadata;
using MyEventApi.Core.Dtos;
using MyEventApi.Core.Entities;
using MyEventApi.Core.Enums;
using MyEventApi.Core.Interfaces;
using System.Diagnostics;

namespace MyEventApi.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICurrentUserService _currentUserService;

        public EventService(IEventRepository eventRepository, ICurrentUserService currentUserService)
        {
            _eventRepository = eventRepository;
            _currentUserService = currentUserService;            
        }
        

        public async Task<EventResponseDto> CreateEvent(CreateEventRequestDto request)
        {
            var storeId = _currentUserService.GetStoreId();

            var eventEntity = new EventEntity
            {
                Id = Guid.NewGuid(),
                StoreId = storeId,
                Title = request.Title,
                Description = request.Description,
                Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc),
                Price = request.Price,
                MaxAttendees = request.MaxAttendees
            };

            await _eventRepository.CreateEvent(eventEntity);

            return new EventResponseDto
            {
                Id = eventEntity.Id,
                Title = eventEntity.Title,
                Description = eventEntity.Description,
                Date = eventEntity.Date,
                Price = eventEntity.Price,
                MaxAttendees = eventEntity.MaxAttendees,
                RegisteredCount = 0
            };
        }        

        public async Task<EventResponseDto> GetCurrentByStoreSlug(string storeSlug)
        {
            var eventEntity = await _eventRepository.GetCurrentBySlug(storeSlug);

            if (eventEntity is null)
                return null;

            var registeredCount = eventEntity.Registrations.Count(r => r.Status != ERegistrationStatus.Cancelled);


            return new EventResponseDto
            {
                Id = eventEntity.Id,
                Title = eventEntity.Title,
                Description = eventEntity.Description,
                Date = eventEntity.Date,
                Price = eventEntity.Price,
                DiscountPercentage = eventEntity.DiscountPercentage,
                MaxAttendees = eventEntity.MaxAttendees,
                RegisteredCount = registeredCount
            };
        }

        public async Task<IEnumerable<EventResponseDto>> GetAllEventsByStore(string storeSlug)
        {
            var events = await _eventRepository.GetAllEventsByStore(storeSlug);

            return events.Select(e => new EventResponseDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                Price = e.Price,
                DiscountPercentage = e.DiscountPercentage,
                MaxAttendees = e.MaxAttendees,
                RegisteredCount = e.Registrations.Count(r => r.Status != ERegistrationStatus.Cancelled),
                Location = e.Location,
                ImageUrl = e.ImageUrl
            });
        }

        public async Task UpdateEvent(Guid eventId, UpdateEventRequestDto request)
        {
            var eventEntity = await _eventRepository.GetById(eventId);

            if (eventEntity is null)
                throw new InvalidOperationException("Evento não encontrado.");

            eventEntity.Title = request.Title;
            eventEntity.Description = request.Description;
            eventEntity.Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc);
            eventEntity.Price = request.Price;
            eventEntity.MaxAttendees = request.MaxAttendees;
            eventEntity.Location = request.Location;
            eventEntity.ImageUrl = request.ImageUrl;
            eventEntity.DiscountPercentage = request.DiscountPercentage;

            await _eventRepository.UpdateEvent(eventEntity);
        }

        public async Task DeleteEvent(Guid eventId)
        {
            var eventEntity = await _eventRepository.GetById(eventId);
            if (eventEntity is null)
                throw new InvalidOperationException("Evento não encontrado.");

            await _eventRepository.DeleteEvent(eventId);
        }

        public async Task<EventResponseDto> GetEventById(Guid eventId)
        {
            var FindEvent = await _eventRepository.GetById(eventId);

            if (FindEvent == null)
                throw new InvalidOperationException("Evento não encontrado");

            return new EventResponseDto
            {
                Title = FindEvent.Title,
                Description = FindEvent.Description,
                Date = FindEvent.Date,
                Price = FindEvent.Price,
                DiscountPercentage = FindEvent.DiscountPercentage,
                MaxAttendees = FindEvent.MaxAttendees,
                Location = FindEvent.Location,
                ImageUrl = FindEvent.ImageUrl
                
            };

        }
    }
}
