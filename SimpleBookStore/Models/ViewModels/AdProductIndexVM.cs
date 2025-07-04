using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBookStore.Models;

namespace SimpleBookStore.Models.ViewModels
{
    public class AdProductIndexVM
    {
        public IEnumerable<Product>? ProductList { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> AuthorList { get; set; }
    }
}
