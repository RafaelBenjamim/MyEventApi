using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MyEventApi.Application.Services;
using MyEventApi.Core.Dtos;
using MyEventApi.Core.Interfaces;
using MyEventApi.Infrastructure.Data;

namespace MyEventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            try
            {
                var response = await _authService.Register(request);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login (LoginRequestDto request)
        {
            try
            {
                var response = await _authService.Login(request);
                return Ok(response);
            }
            catch (InvalidOperationException ex) 
            {
                return Unauthorized(ex.Message);
            }
        }

    }
}
