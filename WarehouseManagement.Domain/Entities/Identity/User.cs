using WarehouseManagement.Domain.Entities.Common;

namespace WarehouseManagement.Domain.Entities.Identity
{
    public class User : SoftDeleteEntity
    {
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? Phone { get; set; }
        public ICollection<StoreUser> StoreUsers { get; set; } = new List<StoreUser>();
    }
}
