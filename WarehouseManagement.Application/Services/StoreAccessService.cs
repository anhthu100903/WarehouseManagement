using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Application.Services.IServices;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Application.Services
{
    public class StoreAccessService : IStoreAccessService
    {
        private readonly IAppDbContext _context;
        public StoreAccessService(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<StoreAccessResult> CheckAccessAsync(Guid userId, Guid storeId)
        {
            // 1) store có tồn tại không
            var storeExists = await _context.Stores
                .AsNoTracking()
                .AnyAsync(x => x.Id == storeId && !x.IsDeleted);

            if (!storeExists)
            {
                return new StoreAccessResult
                {
                    IsAllowed = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Store not found"
                };
            }

            // 2) user có thuộc store không
            var storeUser = await _context.StoreUsers
                .AsNoTracking()
                .Where(x => x.StoreId == storeId && x.UserId == userId)
                .Select(x => new { x.Status, x.RoleId })
                .FirstOrDefaultAsync();

            if (storeUser == null)
            {
                return new StoreAccessResult
                {
                    IsAllowed = false,
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "You are not a member of this store"
                };
            }

            if (storeUser.Status != StoreUserStatus.Active)
            {
                return new StoreAccessResult
                {
                    IsAllowed = false,
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Your store membership is not active"
                };
            }

            return new StoreAccessResult
            {
                IsAllowed = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "OK",
                RoleId = storeUser.RoleId
            };
        }
    }
}
