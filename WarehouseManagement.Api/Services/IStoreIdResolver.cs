namespace WarehouseManagement.Api.Services
{
    public interface IStoreIdResolver
    {
        bool TryResolveStoreId(HttpContext context,out Guid storeId);
    }
}
