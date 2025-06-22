using SimpleBookStore.Models;
using System.Threading.Tasks;

namespace SimpleBookStore.Service.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetActiveCategories();
        Task ToggleActiveAsync(int id, bool isActive);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
