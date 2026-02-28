using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Domain.Entities.Common;
using WarehouseManagement.Domain.Entities.Identity;

namespace WarehouseManagement.Domain.Entities.Catalog
{
    public class ProductCategory : SoftDeleteEntity
    {
        public Guid StoreId { get; set; }
        public Store Store { get; set; } = default!;
        public Guid ParentId { get; set;} = default!;
        public ProductCategory? Parent { get; set; }
        public ICollection<ProductCategory> Children { get; set; } = new List<ProductCategory>();
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;

    }
}
