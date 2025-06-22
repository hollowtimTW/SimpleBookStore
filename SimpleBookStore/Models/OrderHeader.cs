using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookStore.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal OrderTotal { get; set; }
        [MaxLength(50)]
        public string? OrderStatus { get; set; }
        [MaxLength(50)]
        public string? PaymentStatus { get; set; }
        [MaxLength(100)]
        public string? TrackingNumber { get; set; }
        [MaxLength(100)]
        public string? Carrier { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        [MaxLength(100)]
        public string? SessionId { get; set; }
        [MaxLength(100)]
        public string? PaymentIntentId { get; set; }
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(200)]
        public string StreetAddress { get; set; }
        [Required, MaxLength(100)]
        public string City { get; set; }
        [Required, MaxLength(100)]
        public string State { get; set; }
        [Required, MaxLength(20)]
        public string PostalCode { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }

        // Navigation properties

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
