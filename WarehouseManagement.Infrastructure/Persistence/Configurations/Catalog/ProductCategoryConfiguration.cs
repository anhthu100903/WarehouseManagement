using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Domain.Entities.Catalog;

namespace WarehouseManagement.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(pc => pc.Code)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(x => new { x.StoreId, x.Code }).IsUnique();

            builder.HasOne(pc => pc.Store)
                .WithMany()
                .HasForeignKey(pc => pc.StoreId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(pc => pc.Parent)
                .WithMany(pc => pc.Children)
                .HasForeignKey(pc => pc.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
