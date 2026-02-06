using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Domain.Entities.Common;

namespace WarehouseManagement.Domain.Entities.Identity
{
    public class Permission : SoftDeleteEntity
    {
        public string Code { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
