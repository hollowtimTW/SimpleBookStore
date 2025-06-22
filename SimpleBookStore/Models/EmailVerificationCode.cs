namespace SimpleBookStore.Models
{
    public class EmailVerificationCode
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }
    }
}
