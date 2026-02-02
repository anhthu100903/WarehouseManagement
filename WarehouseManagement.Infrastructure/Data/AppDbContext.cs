using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Category> Categories => Set<Category>();
    }
}
