using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility.Helper;

namespace SimpleBookStore.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Category?> GetAsync(int id)
        {
            return await _db.Categories
                .Where(p => !p.IsDeleted && p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetActiveCategories()
        {
            return await _db.Categories
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task ToggleActiveAsync(int id, bool isActive)
        {
            var product = await _db.Categories.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("找不到類別");
            }
            product.IsActive = isActive;
            await _db.SaveChangesAsync();
        }

        public async Task CreateAsync(Category category)
        {
            if (await _db.Categories.AnyAsync(c => c.Name == category.Name))
            {
                throw new InvalidOperationException("類別名稱重複。");
            }
            category.CreatedAt = TimeHelper.Now();
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var originCategory = await _db.Categories.FirstOrDefaultAsync(p => p.Id == category.Id);
            if (originCategory == null)
            {
                throw new Exception("找不到類別");
            }
            if (await _db.Categories.AnyAsync(c => c.Name == category.Name))
            {
                throw new InvalidOperationException("類別名稱重複。");
            }
            originCategory.Name = category.Name;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _db.Categories.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("找不到類別");
            }
            product.IsActive = false;
            product.IsDeleted = true;
            await _db.SaveChangesAsync();
        }
    }
}
