namespace WarehouseManagement.Application.Services.IServices
{
    public interface IStoreAccessService
    {
        Task<StoreAccessResult> CheckAccessAsync(Guid userId, Guid storeId);
    }
    public class StoreAccessResult
    {
        public bool IsAllowed { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid? RoleId { get; set; }
    }
}
