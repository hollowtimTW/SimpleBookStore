using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SimpleBookStore.Models.ViewModels
{
    public class ShoppingCartVM
    {
        [ValidateNever]
        public IEnumerable<ShoppingCart> ShoppingCart { get; set; }
        public int Total { get; set; } = 0;
        [ValidateNever]
        public CouponValidation? CouponValidation { get; set; }
    }

    public class CouponValidation
    {
        public string CouponCode { get; set; }
        public int DiscountAmount { get; set; }
        public int MinimumSpend { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
