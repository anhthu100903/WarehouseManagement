using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.DTOs.Store;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Application.Services.IServices;
using WarehouseManagement.Domain.Entities.Identity;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IAppDbContext _context;
        public StoreService(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateStoreAsync(Guid userId, CreateStoreRequest request)
        {
            await using var transaction = await _context.BeginTransactionAsync();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
                throw new Exception("Người dùng chưa đăng ký");

            var ownerRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "OWNER");
            if(ownerRole == null)
                throw new Exception("Vai trò OWNER chưa được khởi tạo trong hệ thống");

            var store = new Store
            {
                Name = request.Name.Trim(),
                OwnerUserId = userId,
                IsDeleted = false
            };

            _context.Stores.Add(store);

            var storeUser = new StoreUser
            {
                StoreId = store.Id,
                UserId = userId,
                RoleId = ownerRole.Id,
                Status = StoreUserStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            _context.StoreUsers.Add(storeUser);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return store.Id;
        }
    }
}
