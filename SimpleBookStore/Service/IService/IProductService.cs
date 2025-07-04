using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBookStore.Models;
using SimpleBookStore.Models.ViewModels;
using System.Globalization;
using System.Threading.Tasks;

namespace SimpleBookStore.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetAsync(int id);
        Task ToggleActiveAsync(int id, bool isActive);
        Task CreateAsync(Product product, IFormFile? imageFile);
        Task UpdateAsync(Product product, IFormFile? imageFile);
        Task DeleteAsync(int id);
        Task<ProductIndexVM> GetPagedProductsAsync(int limit, int page, string sortBy, string orderBy);
        Task<IEnumerable<Product>> GetByAuthorAsync(int id);
        Task<IEnumerable<Product>> GetByCategoryAsync(int id);
        Task<(IEnumerable<Product> ProductList, int TotalPages, int TotalCount)> SearchProductsAsync(
           string keyword, int? categoryId, int? priceFrom, int? priceTo, string sortBy, int page, int limit);
    }
}
