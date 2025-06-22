using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility.Helper;
using System.Threading.Tasks;

namespace SimpleBookStore.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.Author)
                .ToListAsync();
        }

        public async Task<Product?> GetAsync(int id)
        {
            return await _db.Products
                .Include(p => p.Category)
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task ToggleActiveAsync(int id, bool isActive)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("找不到產品");
            }
            product.IsActive = isActive;
            product.UpdatedAt = TimeHelper.Now();
            await _db.SaveChangesAsync();
        }

        public async Task CreateAsync(Product product)
        {
            product.CreatedAt = TimeHelper.Now();
            product.UpdatedAt = TimeHelper.Now();
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var originProduct = await _db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (originProduct == null)
            {
                throw new Exception("找不到產品");
            }
            originProduct.Title = product.Title;
            originProduct.CategoryId = product.CategoryId;
            originProduct.AuthorId = product.AuthorId;
            originProduct.Publisher = product.Publisher;
            originProduct.PublishedDate = product.PublishedDate;
            originProduct.Price = product.Price;
            originProduct.Description = product.Description;
            originProduct.UpdatedAt = TimeHelper.Now();
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) 
            { 
                throw new Exception("找不到產品"); 
            }
            product.IsActive = false;
            product.IsDeleted = true;
            product.UpdatedAt = TimeHelper.Now();
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId)
        {
            return await _db.ProductImages
                .Where(p => p.ProductId == productId)
                .ToListAsync();
        }
    }
}