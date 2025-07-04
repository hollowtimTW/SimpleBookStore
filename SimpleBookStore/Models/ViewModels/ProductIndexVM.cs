using SimpleBookStore.Models;

namespace SimpleBookStore.Models.ViewModels
{
    public class ProductIndexVM
    {
        public IEnumerable<Product> ProductList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}

