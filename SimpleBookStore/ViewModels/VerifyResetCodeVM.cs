using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.ViewModels
{
    public class VerifyResetCodeVM
    {
        [Required(ErrorMessage = "驗證碼為必填欄位")]
        [Display(Name = "驗證碼")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email 為必填欄位")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
