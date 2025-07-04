using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookStore.Models
{
    public class OrderHeader
    {
        [Key]
        public int OrderHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public int Discount { get; set; }
        public int OrderTotal { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }

        // Navigation properties
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
