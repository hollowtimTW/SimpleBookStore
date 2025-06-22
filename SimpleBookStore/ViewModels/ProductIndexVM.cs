using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBookStore.Models;

namespace SimpleBookStore.ViewModels
{
    public class ProductIndexVM
    {
        public IEnumerable<Product>? ProductList { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> AuthorList { get; set; }
    }
}
