using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Domain.Entities.Identity
{
    public class StoreUser
    {
        public Guid StoreId { get; set; }
        public Store Store { get; set; } = default!;
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
        public StoreUserStatus Status { get; set; } = StoreUserStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
