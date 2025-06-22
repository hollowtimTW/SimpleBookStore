namespace SimpleBookStore.Service.IService
{
    public interface IVerificationCodeService
    {
        Task<string> GenerateVerificationCodeAsync(string userId);
        Task<bool> ValidateVerificationCodeAsync(string userId, string code);
        Task<bool> ValidatePasswordResetCodeAsync(string email, string code);
        Task<string> GeneratePasswordResetCodeAsync(string email);
    }
}
