using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookStore.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }

        // Navigation properties
        [ForeignKey("OrderHeaderId")]
        public OrderHeader? OrderHeader { get; set; }
        [NotMapped]
        public Product? Product { get; set; }
    }
}
