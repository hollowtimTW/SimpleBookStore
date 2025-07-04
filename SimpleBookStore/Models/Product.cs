using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string? Publisher { get; set; }
        public DateTime? PublishedDate { get; set; }
        [Required]
        public int Price { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ValidateNever]
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
