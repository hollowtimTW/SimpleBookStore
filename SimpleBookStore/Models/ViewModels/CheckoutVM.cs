using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.Models.ViewModels
{
    public class CheckoutVM
    {
        [ValidateNever]
        public List<ShoppingCart> CartItems { get; set; }
        [ValidateNever]
        public int Total { get; set; }

        public RecipientInfo Recipient { get; set; }
        [ValidateNever]
        public CouponResult? CouponResult { get; set; }
    }

    public class RecipientInfo
    {
        [Required(ErrorMessage = "姓名需填寫")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "手機號碼需填寫")]
        [Display(Name = "手機號碼")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "地址需填寫")]
        [Display(Name = "地址")]
        public string Address { get; set; }
    }

    public class CouponResult
    {
        public string CouponCode { get; set; }
        public int DiscountAmount { get; set; }
    }
}
