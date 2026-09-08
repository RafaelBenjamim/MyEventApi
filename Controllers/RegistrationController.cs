using Microsoft.AspNetCore.Mvc;
using MyEventApi.Core.Dtos;
using MyEventApi.Core.Interfaces;

namespace MyEventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {

        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
  
        [HttpPost]
        public async Task<IActionResult> CreateRegistration(CreateRegistrationRequest request)
        {
            try
            {
                var response = await _registrationService.CreateRegistration(request);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConfirmationEvent(Guid id)
        {
            try
            {
                var response = await _registrationService.GetConfirmationEvent(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetRegistrationsByEvent(Guid eventId)
        {
            try
            {
                var response = await _registrationService.GetRegistrationsByEvent(eventId);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
