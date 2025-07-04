using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.Models.ViewModels
{
    public class VerifyEmailVM
    {
        [Required(ErrorMessage = "驗證碼為必填欄位")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "驗證碼必須是6位數")]
        [Display(Name = "驗證碼")]
        public string Code { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
