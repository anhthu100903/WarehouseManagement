

using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities.Identity;
using WarehouseManagement.Infrastructure.Persistence;

namespace WarehouseManagement.Infrastructure.Seed
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (!await db.Roles.AnyAsync())
            {
                var owner = new Role { Name = "OWNER", Description = "Chủ cửa hàng" };
                var manager = new Role { Name = "MANAGER", Description = "Quản lý cửa hàng" };
                var staff = new Role { Name = "STAFF", Description = "Nhân viên cửa hàng" };

                await db.Roles.AddRangeAsync(owner, manager, staff);
                await db.SaveChangesAsync();
            }

            if (!await db.Permissions.AnyAsync())
            {
                var permissions = new List<Permission>
                {
                    new Permission { Code = "PRODUCT_VIEW", Description = "Xem sản phẩm"},
                    new Permission { Code = "PRODUCT_CREATE", Description = "Tạo sản phẩm"},
                    new Permission { Code = "PRODUCT_EDIT", Description = "Chỉnh sửa sản phẩm"},
                    new Permission { Code = "PRODUCT_DELETE", Description = "Xóa sản phẩm"},

                    new Permission { Code = "ORDER_VIEW", Description = "Xem đơn hàng"},
                    new Permission { Code = "ORDER_CREATE", Description = "Tạo đơn hàng"},
                    new Permission { Code = "ORDER_EDIT", Description = "Chỉnh sửa đơn hàng"},
                    new Permission { Code = "ORDER_DELETE", Description = "Xóa đơn hàng"},

                    new Permission { Code = "CATEGORY_VIEW", Description = "Xem danh mục"},
                    new Permission { Code = "CATEGORY_CREATE", Description = "Tạo danh mục"},
                    new Permission { Code = "CATEGORY_EDIT", Description = "Chỉnh sửa danh mục"},
                    new Permission { Code = "CATEGORY_DELETE", Description = "Xóa danh mục"},

                    new() { Code = "STORE_USER_INVITE", Description = "Mời user vào store" },
                    new() { Code = "STORE_USER_REMOVE", Description = "Xoá user khỏi store" },
                };
                await db.Permissions.AddRangeAsync(permissions);
                await db.SaveChangesAsync();
            }

            // Gán permission cho role
            // OWNER: full
            // MANAGER: gần full trừ quyền quản trị user
            // STAFF: chỉ view + tạo đơn
            if (!await db.RolePermissions.AnyAsync())
            {
                var owner = await db.Roles.FirstAsync(x => x.Name == "OWNER");
                var manager = await db.Roles.FirstAsync(x => x.Name == "MANAGER");
                var staff = await db.Roles.FirstAsync(x => x.Name == "STAFF");

                var allPermissions = await db.Permissions.ToListAsync();

                // OWNER = tất cả
                var ownerLinks = allPermissions.Select(p => new RolePermission
                {
                    RoleId = owner.Id,
                    PermissionId = p.Id
                });

                // MANAGER = tất cả trừ remove user (ví dụ)
                var managerPerms = allPermissions
                    .Where(p => p.Code != "STORE_USER_REMOVE")
                    .ToList();

                var managerLinks = managerPerms.Select(p => new RolePermission
                {
                    RoleId = manager.Id,
                    PermissionId = p.Id
                });

                // STAFF = chỉ view
                var staffPerms = allPermissions
                    .Where(p => p.Code.EndsWith("_VIEW"))
                    .ToList();

                var staffLinks = staffPerms.Select(p => new RolePermission
                {
                    RoleId = staff.Id,
                    PermissionId = p.Id
                });

                await db.RolePermissions.AddRangeAsync(ownerLinks);
                await db.RolePermissions.AddRangeAsync(managerLinks);
                await db.RolePermissions.AddRangeAsync(staffLinks);

                await db.SaveChangesAsync();
            }
            await Task.CompletedTask;
        }
    }
}
