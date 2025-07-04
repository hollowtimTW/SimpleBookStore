using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBookStore.Models;

namespace SimpleBookStore.Models.ViewModels
{
    public class AdProductVM
    {
        public Product? Product { get; set; }
        [ValidateNever]
        public List<SelectListItem> CategoryList { get; set; }
        [ValidateNever]
        public List<SelectListItem> AuthorList { get; set; }
    }
}
