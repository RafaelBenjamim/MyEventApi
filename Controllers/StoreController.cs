using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEventApi.Core.Dtos;
using MyEventApi.Core.Interfaces;

namespace MyEventApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService) => _storeService = storeService;

        [HttpPut("payment-settings")]
        public async Task<IActionResult> UpdatePaymentSettings(UpdatePaymentSettingsRequest request)
        {
            try
            {
                await _storeService.UpdatePaymentSettings(request);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
