
namespace WarehouseManagement.Api.Services
{
    public class RouteStoreIdResolver : IStoreIdResolver
    {
        public bool TryResolveStoreId(HttpContext context, out Guid storeId)
        {
            storeId = Guid.Empty;

            if (!context.Request.RouteValues.TryGetValue("storeId", out var storeIdObj))
                return false;

            return Guid.TryParse(storeIdObj?.ToString(), out storeId);
        }
    }
}
