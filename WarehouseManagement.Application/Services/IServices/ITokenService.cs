using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Domain.Entities.Identity;

namespace WarehouseManagement.Application.Services.IServices
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
