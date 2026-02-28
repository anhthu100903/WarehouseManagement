using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement.Application.DTOs.Auth
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public Guid? UserId { get; set; }
        public string? Token { get; set; }
    }
}
