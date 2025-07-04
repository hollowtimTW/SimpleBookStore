using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "新密碼為必填欄位")]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "確認新密碼")]
        [Compare("NewPassword", ErrorMessage = "密碼與確認密碼不相符")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
