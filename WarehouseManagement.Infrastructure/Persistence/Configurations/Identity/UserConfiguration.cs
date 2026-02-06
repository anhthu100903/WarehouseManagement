using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManagement.Domain.Entities.Identity;

namespace WarehouseManagement.Infrastructure.Persistence.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.PasswordHash).IsRequired();

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(200); 

            builder.Property(u => u.Phone).HasMaxLength(20);

            builder.Property(u => u.RowVersion).IsRowVersion();
        }
    }
}
