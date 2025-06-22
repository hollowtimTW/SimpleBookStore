using SimpleBookStore.Models;

namespace SimpleBookStore.Service.IService
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetAsync(int id);
        Task CreateAsync(Author author, IFormFile imageFile);
        Task UpdateAsync(Author author, IFormFile imageFile);
    }
}
