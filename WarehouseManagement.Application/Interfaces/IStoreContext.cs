using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement.Application.Interfaces
{
    public interface IStoreContext
    {
        Guid StoreId { get; set; }
        Guid UserId { get; set; }
        Guid RoleId { get; set; }
    }
}
