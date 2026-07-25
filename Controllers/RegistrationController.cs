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
    }
}
