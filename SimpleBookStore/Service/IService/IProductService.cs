using SimpleBookStore.Models;

namespace SimpleBookStore.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetAsync(int id);
        Task ToggleActiveAsync(int id, bool isActive);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId);
    }
}
