namespace WarehouseManagement.Domain.Entities.Common
{
    public abstract class SoftDeleteEntity : BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
