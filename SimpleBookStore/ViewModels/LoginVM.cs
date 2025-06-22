using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email 為必填欄位")]
        [EmailAddress(ErrorMessage = "請輸入有效的 Email 地址")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "密碼為必填欄位")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "記住我")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
