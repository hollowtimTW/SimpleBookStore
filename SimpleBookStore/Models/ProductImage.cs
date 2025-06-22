using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookStore.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }
        [Required]
        public int SortOrder { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
