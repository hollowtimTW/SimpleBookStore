using SimpleBookStore.Utility;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "全名為必填欄位")]
        [Display(Name = "全名")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email 為必填欄位")]
        [EmailAddress(ErrorMessage = "請輸入有效的 Email 地址")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "密碼為必填欄位")]
        [StringLength(100, ErrorMessage = "密碼長度至少需要 {2} 個字元", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼與確認密碼不相符")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "地址為必填欄位")]
        [Display(Name = "地址")]
        public string Address { get; set; } = string.Empty;

        [Phone(ErrorMessage = "請輸入有效的電話號碼")]
        [Display(Name = "電話號碼")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "角色")]
        public string Role { get; set; } = SD.Role_Customer;
    }
}
