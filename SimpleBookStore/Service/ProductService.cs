using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Models.ViewModels;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility.Helper;

namespace SimpleBookStore.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ProductService(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
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

        public async Task CreateAsync(Product product, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                product.ImageUrl = await SaveImageAsync(imageFile);
            }
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product, IFormFile imageFile)
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

            // 若有新圖片
            if (imageFile != null && imageFile.Length > 0)
            {
                // 刪除舊圖片（如果有）
                if (!string.IsNullOrEmpty(originProduct.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath, originProduct.ImageUrl.TrimStart('/'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                // 儲存新圖片並更新資料
                originProduct.ImageUrl = await SaveImageAsync(imageFile);
            }
            await _db.SaveChangesAsync();
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadDir = "uploads/product";
            var uploadPath = Path.Combine(_env.WebRootPath, uploadDir);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var ss = Guid.NewGuid().ToString();

            var fileName = ss + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/" + uploadDir + "/" + fileName;
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

        public async Task<ProductIndexVM> GetPagedProductsAsync(int limit, int page, string sortBy, string orderBy)
        {
            var query = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Where(p => p.IsActive && !p.IsDeleted);

            switch (sortBy)
            {
                case "lowest_price":
                    query = (orderBy == "asc") ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                    break;
                case "created_at":
                default:
                    query = (orderBy == "asc") ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt);
                    break;
            }

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)limit);

            var products = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return new ProductIndexVM
            {
                ProductList = products,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = limit,
                OrderBy = orderBy,
                SortBy = sortBy
            };
        }

        public async Task<IEnumerable<Product>> GetByAuthorAsync(int id)
        {
            return await _db.Products
                .Where(p => p.AuthorId == id && p.IsActive && !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int id)
        {
            return await _db.Products
                .Where(p => p.CategoryId == id && p.IsActive && !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.Author)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Product> ProductList, int TotalPages, int TotalCount)> SearchProductsAsync(
           string keyword, int? categoryId, int? priceFrom, int? priceTo, string sortBy, int page, int limit)
        {
            var query = _db.Products
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.IsActive);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p =>
                    p.Title.Contains(keyword) ||
                    (p.Author != null && p.Author.Name.Contains(keyword))
                );
            }
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }
            if (priceFrom.HasValue)
            {
                query = query.Where(p => p.Price >= priceFrom.Value);
            }
            if (priceTo.HasValue)
            {
                query = query.Where(p => p.Price <= priceTo.Value);
            }

            switch (sortBy)
            {
                case "price_asc":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case "date_desc":
                default:
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)limit);
            page = page < 1 ? 1 : page;
            if (totalPages > 0 && page > totalPages) page = totalPages;

            var productList = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (productList, totalPages, totalCount);
        }
    }
}