using WarehouseManagement.Domain.Entities.Common;

namespace WarehouseManagement.Domain.Entities.Identity
{
    public class Store : SoftDeleteEntity
    {
        public string Name { get; set; } = default!;
        public Guid OwnerUserId { get; set; }
        public User OwnerUser { get; set; } = default!;
        public ICollection<StoreUser> StoreUsers { get; set; } = new List<StoreUser>();
    }
}
