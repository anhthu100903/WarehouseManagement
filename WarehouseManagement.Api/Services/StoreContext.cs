using WarehouseManagement.Application.Interfaces;

namespace WarehouseManagement.Api.Services
{
    public class StoreContext : IStoreContext
    {
        public Guid StoreId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
