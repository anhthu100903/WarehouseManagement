using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Application.DTOs.Store;

namespace WarehouseManagement.Application.Services.IServices
{
    public interface IStoreService
    {
        Task<Guid> CreateStoreAsync(Guid userId,CreateStoreRequest request);
    }
}
