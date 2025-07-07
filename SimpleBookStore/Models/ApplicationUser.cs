using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public override string? PhoneNumber { get; set; }
        public bool IsBanned { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
    }
}
  