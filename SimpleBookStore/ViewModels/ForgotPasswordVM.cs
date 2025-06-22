using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Email 為必填欄位")]
        [EmailAddress(ErrorMessage = "請輸入有效的 Email 地址")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
    }
}
