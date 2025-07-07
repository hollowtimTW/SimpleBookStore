using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleBookStore.Models;
using SimpleBookStore.Models.ViewModels;
using SimpleBookStore.Service.IService;
using SimpleBookStore.Utility;
using SimpleBookStore.Utility.Helper;

namespace SimpleBookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _cache;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IVerificationCodeService verificationCodeService,
            IEmailService emailService,
            IMemoryCache cache)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _verificationCodeService = verificationCodeService;
            _emailService = emailService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await EnsureRolesExistAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    CreatedAt = TimeHelper.Now(),
                };

                await EnsureRolesExistAsync();

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(model.Role))
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                    }

                    var code = await _verificationCodeService.GenerateVerificationCodeAsync(user.Id);

                    await _emailService.SendEmailAsync(user.Email!,
                        "Email 驗證碼",
                        $@"
                        <h3>歡迎註冊！</h3>
                        <p>您的 Email 驗證碼是：</p>
                        <h2 style='color: #007bff; font-family: monospace;'>{code}</h2>
                        <p>此驗證碼將在 10 分鐘後過期。</p>
                        <p>請在註冊頁面輸入此驗證碼以完成註冊。</p>
                        ");
                    var cooldownKey = $"resend_cooldown_{user.Id}";
                    _cache.Set(cooldownKey, DateTime.UtcNow, TimeSpan.FromSeconds(60));

                    return RedirectToAction("VerifyEmail", new { userId = user.Id, email = user.Email });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string userId, string email)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Register");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Register");
            }

            if (user.EmailConfirmed)
            {
                TempData["Message"] = "您的 Email 已經驗證過了，可以直接登入。";
                return RedirectToAction("Login");
            }

            var model = new VerifyEmailVM
            {
                UserId = userId,
                Email = email
            };

            // 檢查冷卻時間
            var cooldownKey = $"resend_cooldown_{userId}";
            if (_cache.TryGetValue(cooldownKey, out DateTime cooldownStart))
            {
                var elapsed = DateTime.UtcNow - cooldownStart;
                if (elapsed.TotalSeconds < 60)
                {
                    ViewData["CooldownSeconds"] = (int)(60 - elapsed.TotalSeconds);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(VerifyEmailVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return RedirectToAction("Register");
                }

                // 如果已經驗證過，直接跳轉
                if (user.EmailConfirmed)
                {
                    TempData["Message"] = "您的 Email 已經驗證過了，可以直接登入。";
                    return RedirectToAction("Login");
                }

                var isValid = await _verificationCodeService.ValidateVerificationCodeAsync(model.UserId, model.Code);

                if (isValid)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);

                    // 驗證成功後清除冷卻記錄
                    _cache.Remove($"resend_cooldown_{model.UserId}");

                    // 驗證成功後導向登入頁，避免返回驗證頁繼續操作
                    TempData["Message"] = "Email 驗證成功！您現在可以登入了。";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, "驗證碼無效或已過期。");
            }

            var cooldownKey = $"resend_cooldown_{model.UserId}";
            if (_cache.TryGetValue(cooldownKey, out DateTime cooldownStart))
            {
                var elapsed = DateTime.UtcNow - cooldownStart;
                if (elapsed.TotalSeconds < 60)
                {
                    ViewData["CooldownSeconds"] = (int)(60 - elapsed.TotalSeconds);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendVerificationCode(string userId, string email)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.Email == email)
            {
                // 如果已經驗證過，不再發送
                if (user.EmailConfirmed)
                {
                    TempData["Message"] = "您的 Email 已經驗證過了，可以直接登入。";
                    return RedirectToAction("Login");
                }

                var cooldownKey = $"resend_cooldown_{userId}";
                if (_cache.TryGetValue(cooldownKey, out _))
                {
                    TempData["ErrorMessage"] = "請等待 60 秒後再重新發送。";
                    return RedirectToAction("VerifyEmail", new { userId = userId, email = email });
                }

                var code = await _verificationCodeService.GenerateVerificationCodeAsync(user.Id);

                await _emailService.SendEmailAsync(user.Email!,
                    "Email 驗證碼（重新發送）",
                    $@"
                    <h3>重新發送驗證碼</h3>
                    <p>您的 Email 驗證碼是：</p>
                    <h2 style='color: #007bff; font-family: monospace;'>{code}</h2>
                    <p>此驗證碼將在 10 分鐘後過期。</p>
                    ");

                // 設定 60 秒的冷卻時間，會自動過期
                _cache.Set(cooldownKey, DateTime.UtcNow, TimeSpan.FromSeconds(60));

                TempData["Message"] = "驗證碼已重新發送到您的 Email。";
            }

            return RedirectToAction("VerifyEmail", new { userId = userId, email = email });
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // 檢查 Email 是否已驗證
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        // 檢查冷卻時間
                        var cooldownKey = $"resend_cooldown_{user.Id}";
                        if (!_cache.TryGetValue(cooldownKey, out _))
                        {
                            var code = await _verificationCodeService.GenerateVerificationCodeAsync(user.Id);

                            await _emailService.SendEmailAsync(user.Email!,
                                "Email 驗證碼",
                                $@"
                        <h3>請驗證您的 Email</h3>
                        <p>您需要先驗證 Email 才能登入。您的驗證碼是：</p>
                        <h2 style='color: #007bff; font-family: monospace;'>{code}</h2>
                        <p>此驗證碼將在 10 分鐘後過期。</p>
                        ");

                            _cache.Set(cooldownKey, DateTime.UtcNow, TimeSpan.FromSeconds(60));
                            TempData["Message"] = "請先驗證您的 Email。我們已重新發送驗證碼到您的信箱。";
                        }
                        else
                        {
                            TempData["Message"] = "請先驗證您的 Email。";
                        }

                        return RedirectToAction("VerifyEmail", new { userId = user.Id, email = user.Email });
                    }

                    var result = await _signInManager.PasswordSignInAsync(
                        user.UserName!,
                        model.Password,
                        model.RememberMe,
                        lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        if (returnUrl != null && returnUrl.Contains("Cart"))
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        return RedirectToLocal(returnUrl);
                    }
                    else if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "帳戶已被鎖定，請稍後再試。");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "您的帳戶尚未允許登入。");
                    }
                    else
                    {
                        var failedCount = await _userManager.GetAccessFailedCountAsync(user);
                        var maxAttempts = 5; // 設定最大次數
                        var remaining = maxAttempts - failedCount;
                        if (remaining > 0)
                        {
                            ModelState.AddModelError(string.Empty, $"Email 或密碼錯誤。再錯誤 {remaining} 次帳戶將被鎖定。");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email 或密碼錯誤。");
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // 不透露用戶是否存在
                    ViewBag.Message = "請檢查您的 Email 以重設密碼。";
                    return View("Info");
                }

                var code = await _verificationCodeService.GeneratePasswordResetCodeAsync(model.Email);

                await _emailService.SendEmailAsync(user.Email!,
                    "密碼重設驗證碼",
                    $@"
                    <h3>密碼重設請求</h3>
                    <p>您的密碼重設驗證碼是：</p>
                    <h2 style='color: #dc3545; font-family: monospace;'>{code}</h2>
                    <p>此驗證碼將在 15 分鐘後過期。</p>
                    <p>請在重設密碼頁面輸入此驗證碼。</p>
                    ");

                return RedirectToAction("VerifyResetCode", new { email = model.Email });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyResetCode(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword");
            }

            // 檢查是否有有效的重設碼存在
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("ForgotPassword");
            }

            var model = new VerifyResetCodeVM { Email = email };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyResetCode(VerifyResetCodeVM model)
        {
            if (ModelState.IsValid)
            {
                var isValid = await _verificationCodeService.ValidatePasswordResetCodeAsync(model.Email, model.Code);

                if (isValid)
                {
                    // 生成臨時 token 用於重設密碼
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        return RedirectToAction("ResetPassword", new { email = model.Email, token = token });
                    }
                }

                ModelState.AddModelError(string.Empty, "驗證碼無效或已過期。");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (email == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordVM { Email = email, Token = token };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ViewBag.Message = "密碼重設成功。";
                    return View("Info");
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    ViewBag.Message = "密碼重設成功。您現在可以使用新密碼登入。";
                    return View("Info");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private async Task EnsureRolesExistAsync()
        {
            if (_roleManager == null) return;

            var roles = new[] { SD.Role_Admin, SD.Role_Customer };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ApplicationUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            user.FullName = model.FullName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "個人資料已更新";
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(user);
        }
    }
}