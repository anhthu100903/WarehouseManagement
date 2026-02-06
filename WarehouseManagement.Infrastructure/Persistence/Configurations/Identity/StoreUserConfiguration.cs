using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagement.Domain.Entities.Identity;

namespace WarehouseManagement.Infrastructure.Persistence.Configurations.Identity
{
    public class StoreUserConfiguration : IEntityTypeConfiguration<StoreUser>
    {
        public void Configure(EntityTypeBuilder<StoreUser> builder)
        {
            builder.ToTable("StoreUsers");

            builder.HasKey(su => new { su.StoreId, su.UserId });

            builder.HasOne(su => su.Store)
                .WithMany(su => su.StoreUsers)
                .HasForeignKey(su => su.StoreId);

            builder.HasOne(su => su.User)
                .WithMany(su => su.StoreUsers)
                .HasForeignKey(su => su.UserId);

            builder.HasOne(su => su.Role)
                .WithMany(su => su.StoreUsers)
                .HasForeignKey(su => su.RoleId);

            builder.HasIndex(su => su.RoleId);
            builder.HasIndex(su => su.UserId);
        }
    }
}
