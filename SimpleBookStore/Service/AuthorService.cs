using Microsoft.EntityFrameworkCore;
using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility.Helper;

namespace SimpleBookStore.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public AuthorService(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _db.Authors.ToListAsync();
        }

        public async Task<Author?> GetAsync(int id)
        {
            return await _db.Authors.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateAsync(Author author, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                author.ImageUrl = await SaveImageAsync(imageFile);
            }
            _db.Authors.Add(author);
            await _db.SaveChangesAsync();
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadDir = "uploads/author";
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
    }
}
