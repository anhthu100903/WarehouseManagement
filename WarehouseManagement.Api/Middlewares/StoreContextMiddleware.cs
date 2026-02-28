
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WarehouseManagement.Api.Services;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Application.Services.IServices;

namespace WarehouseManagement.Api.Middlewares
{
    public class StoreContextMiddleware : IMiddleware
    {
        private readonly IStoreAccessService _storeAccessService;
        private readonly IStoreIdResolver _storeIdResolver;
        private readonly IStoreContext _storeContext;
        public StoreContextMiddleware(
            IStoreAccessService storeAccessService,
            IStoreIdResolver storeIdResolver,
            IStoreContext storeContext)
        {
            _storeAccessService = storeAccessService;
            _storeIdResolver = storeIdResolver;
            _storeContext = storeContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Chỉ validate khi route có storeId
            if (!_storeIdResolver.TryResolveStoreId(context, out var storeId))
            {
                await next(context);
                return;
            }

            // User phải đăng nhập
            var userIdStr = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
                return;
            }

            // Check quyền store
            var result = await _storeAccessService.CheckAccessAsync(storeId, userId);

            if (!result.IsAllowed)
            {
                context.Response.StatusCode = result.StatusCode;
                await context.Response.WriteAsJsonAsync(new { message = result.Message });
                return;
            }

            if (result.RoleId == null)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "Không xác định được vai trò trong cửa hàng" });
                return;
            }

            // Gán vào StoreContext để service dùng được
            _storeContext.StoreId = storeId;
            _storeContext.UserId = userId;
            _storeContext.RoleId = result.RoleId.Value;

            await next(context);
        }
    }
}
