using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true; 
        public bool IsDeleted { get; set; } = false;
        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }
}
