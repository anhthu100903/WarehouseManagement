using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces;
using WarehouseManagement.Infrastructure.Data;

namespace WarehouseManagement.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryResponseDto>> GetAllAsync()
        {
            return await _context.Categories
                .Where(x=> !x.IsDeleted)
                .OrderByDescending(x=> x.CreatedAt)
                .Select(x => new CategoryResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync();
        }
        public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
        {
            return await _context.Categories
                .Where(x => x.Id == id)
                .Select(x => new CategoryResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .FirstOrDefaultAsync();
        }
        public async Task<Guid> CreateAsync(CreateCategoryRequest request)
        {
            // 1. Kiểm tra xem tên đã tồn tại chưa (Tránh trùng lặp)
            var isExisted = await _context.Categories
                .AnyAsync(x => x.Name == request.Name && !x.IsDeleted);
            if (isExisted)
            {
                throw new Exception("Tên danh mục này đã tồn tại."); // Bạn nên dùng Custom Exception
            }

            var entity = new Domain.Entities.Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Slug = request.Slug.Trim() ?? GenerateSlug(request.Name),
                Description = request.Description,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request)
        {
            var entity = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity == null) return false;

            entity.Name = request.Name.Trim();
            entity.Slug = request.Slug.Trim();
            entity.Description = request.Description;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        // Add this private method to the CategoryService class to fix CS0103
        private string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            // Simple slug generation: lowercase, trim, replace spaces with hyphens, remove invalid chars
            var slug = name.Trim().ToLowerInvariant();
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");
            return slug;
        }
    }
}
