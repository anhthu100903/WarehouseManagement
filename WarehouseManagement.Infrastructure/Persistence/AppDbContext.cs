using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WarehouseManagement.Domain.Entities.Identity.Store> Stores { get; set; } = default!;
        public DbSet<WarehouseManagement.Domain.Entities.Identity.User> Users { get; set; } = default!;
        public DbSet<WarehouseManagement.Domain.Entities.Identity.StoreUser> StoreUsers { get; set; } = default!;
        public DbSet<WarehouseManagement.Domain.Entities.Identity.Role> Roles { get; set; } = default!;
        public DbSet<WarehouseManagement.Domain.Entities.Identity.Permission> Permissions { get; set; } = default!;
        public DbSet<WarehouseManagement.Domain.Entities.Identity.RolePermission> RolePermissions { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
