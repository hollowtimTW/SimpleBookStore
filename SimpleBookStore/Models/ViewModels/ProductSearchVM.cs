using SimpleBookStore.Models;

namespace SimpleBookStore.Models.ViewModels
{
    public class ProductSearchVM
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string SortBy { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}

