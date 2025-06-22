using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBookStore.Models;

namespace SimpleBookStore.ViewModels
{
    public class ProductVM
    {
        public Product? Product { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<SelectListItem> AuthorList { get; set; }
    }
}
