using WarehouseManagement.Domain.Entities.Common;

namespace WarehouseManagement.Domain.Entities.Identity
{
    public class Role : SoftDeleteEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public ICollection<StoreUser> StoreUsers { get; set; } = new List<StoreUser>();
    }
}
