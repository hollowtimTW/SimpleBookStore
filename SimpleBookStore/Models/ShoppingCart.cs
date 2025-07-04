using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookStore.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int UnitPrice { get; set; }

        // Navigation properties
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
