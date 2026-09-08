using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyEventApi.Core.Dtos;
using MyEventApi.Core.Interfaces;

namespace MyEventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent(CreateEventRequestDto request)
        {
            var response =  await _eventService.CreateEvent(request);
            return Ok(response);
        }

        [HttpGet("current/{storeSlug}")]
        public async Task<IActionResult> GetCurrent(string storeSlug)
        {
            var response = await _eventService.GetCurrentByStoreSlug(storeSlug);
            if (response is null)
                return NotFound("Nenhum evento ativo no momento");

            return Ok(response);
        }

        [HttpGet("{storeSlug}")]
        public async Task<IActionResult> GetAllByStore(string storeSlug)
        {
            var response = await _eventService.GetAllEventsByStore(storeSlug);

            return Ok(response);
        }

        [HttpPut("UpdateEvent/{eventId}")]
        public async Task<IActionResult> UpdateEvent(Guid eventId, UpdateEventRequestDto request)
        {
            await _eventService.UpdateEvent(eventId, request);
            return Ok();
        }

        [HttpDelete("DeleteEvent/{eventId}")]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {
            await _eventService.DeleteEvent(eventId);
            return Ok();
        }

        [HttpGet("GetEventById/{eventId}")]
        public async Task<IActionResult> GetEventById(Guid eventId)
        {
            var response = await _eventService.GetEventById(eventId);
            return Ok(response);
        }
    }
}
