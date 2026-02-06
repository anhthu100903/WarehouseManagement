using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WarehouseManagement.Application.DTOs.Store;
using WarehouseManagement.Application.Services.IServices;

namespace WarehouseManagement.Api.Controllers
{

    [ApiController]
    [Route("api/stores")]
    [Authorize]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStoreRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                                   ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

            var storeId = await _storeService.CreateStoreAsync(userId, request);

            return Ok(new { StoreId = storeId });
        }
    }
}
