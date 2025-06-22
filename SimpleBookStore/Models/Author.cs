using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }
}
