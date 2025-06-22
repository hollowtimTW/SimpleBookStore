using SimpleBookStore.Data;
using SimpleBookStore.Models;
using SimpleBookStore.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace SimpleBookStore.Service
{
    public class VerificationCodeService : IVerificationCodeService
    {
        private readonly AppDbContext _db;

        public VerificationCodeService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<string> GenerateVerificationCodeAsync(string userId)
        {
            // 移除該用戶的舊驗證碼
            var oldCodes = await _db.EmailVerificationCodes
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _db.EmailVerificationCodes.RemoveRange(oldCodes);

            // 生成新的 6 位數驗證碼
            var code = GenerateRandomCode();

            var verificationCode = new EmailVerificationCode
            {
                UserId = userId,
                Code = code,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10), // 10 分鐘過期
                IsUsed = false
            };

            _db.EmailVerificationCodes.Add(verificationCode);
            await _db.SaveChangesAsync();

            return code;
        }

        public async Task<bool> ValidateVerificationCodeAsync(string userId, string code)
        {
            var verificationCode = await _db.EmailVerificationCodes
                .FirstOrDefaultAsync(c => c.UserId == userId &&
                                         c.Code == code &&
                                         !c.IsUsed &&
                                         c.ExpiresAt > DateTime.UtcNow);

            if (verificationCode == null)
                return false;

            // 標記為已使用
            verificationCode.IsUsed = true;
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<string> GeneratePasswordResetCodeAsync(string email)
        {
            // 移除該 Email 的舊重設碼
            var oldCodes = await _db.EmailVerificationCodes
                .Where(c => c.User.Email == email && c.Code.StartsWith("RST"))
                .ToListAsync();

            _db.EmailVerificationCodes.RemoveRange(oldCodes);

            // 生成新的重設碼（加上前綴以區分）
            var code = "RST" + GenerateRandomCode();

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return string.Empty;

            var verificationCode = new EmailVerificationCode
            {
                UserId = user.Id,
                Code = code,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15), // 15 分鐘過期
                IsUsed = false
            };

            _db.EmailVerificationCodes.Add(verificationCode);
            await _db.SaveChangesAsync();

            return code;
        }

        public async Task<bool> ValidatePasswordResetCodeAsync(string email, string code)
        {
            var verificationCode = await _db.EmailVerificationCodes
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.Email == email &&
                                         c.Code == code &&
                                         !c.IsUsed &&
                                         c.ExpiresAt > DateTime.UtcNow);

            if (verificationCode == null)
                return false;

            // 標記為已使用
            verificationCode.IsUsed = true;
            await _db.SaveChangesAsync();

            return true;
        }

        private string GenerateRandomCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // 6 位數驗證碼
        }
    }
}
