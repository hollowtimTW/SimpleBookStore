using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; } // 優惠碼
        [MaxLength(200)]
        public string? Description { get; set; } // 優惠卷描述（可選）
        [Required]
        public int DiscountAmount { get; set; } // 折扣金額
        public int MinimumSpend { get; set; } =  0;// 最低消費金額
        [Required]
        public DateTime StartDate { get; set; } // 開始日期
        [Required]
        public DateTime EndDate { get; set; } // 結束日期
        [Required]
        public bool IsActive { get; set; } = true; // 是否啟用

        // Navigation properties

    }
}
