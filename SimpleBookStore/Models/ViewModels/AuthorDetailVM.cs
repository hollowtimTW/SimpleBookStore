using SimpleBookStore.Models;

namespace SimpleBookStore.Models.ViewModels
{
    public class AuthorDetailVM
    {
        public Author? Author { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
