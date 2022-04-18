using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UrlEncoder _encoder;
        public UserController(IApplicationUserManager userManager, SignInManager<ApplicationUser> SignInManager, UrlEncoder encoder)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
            _encoder = encoder;
        }
        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            return View(await LoadSharedKeyAndQrCodeUriAsync(user));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel VM)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
                return View(await LoadSharedKeyAndQrCodeUriAsync(user));
            var Code = VM.Code.Replace(" ", string.Empty).Replace("-", string.Empty);
            var Result = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, Code);
            if (!Result)
            {
                ModelState.AddModelError(string.Empty, "کد اعتبارسنجی نامعتبر است.");
                return View(await LoadSharedKeyAndQrCodeUriAsync(user));
            }
            // مشخص می کند کاربر احراز هویت دو مرحله ای را فعال کرده یانه (تایید احراز هویت = true)
            await _userManager.SetTwoFactorEnabledAsync(user, true);
            if (await _userManager.CountRecoveryCodesAsync(user) == 0)
            {
                var RecoveryCode = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                ViewBag.MSG = "اپلیکشن احراز هویت شما تایید شده است";
                return View("ShowRecoveryCodes", RecoveryCode);
            }
            else
            {
                return RedirectToAction("TwoFactorAuthentication", new { alert = "Success" });
            }
        }
        public string FormatKey(string UnFormatKey)
        {
            StringBuilder Result = new StringBuilder();
            int CurrentPosition = 0;
            while (CurrentPosition + 4 < UnFormatKey.Length)
            {
                Result.Append(UnFormatKey.Substring(CurrentPosition, 4));
                Result.Append(" ");
                CurrentPosition += 4;
            }
            if (CurrentPosition < UnFormatKey.Length)
            {
                Result.Append(CurrentPosition);
            }
            return Result.ToString().ToLowerInvariant();

        }
        public string GenerateQrCodeUri(string UnFormatedKey, string Email)
        {
            string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
            return string.Format(AuthenticatorUriFormat, _encoder.Encode("BookShop"), _encoder.Encode(Email), UnFormatedKey);
        }
        public async Task<EnableAuthenticatorViewModel> LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user)
        {
            var UnFormatedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(UnFormatedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                UnFormatedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }
            var VM = new EnableAuthenticatorViewModel()
            {
                AuthenticatorUri = GenerateQrCodeUri(UnFormatedKey, user.Email),
                SharedKey = FormatKey(UnFormatedKey)
            };
            return VM;
        }
        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication(string alert)
        {
            if (alert == "Success")
                ViewBag.MSG = "اپلیکشن احراز هویت شما تایید شده است";
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            return View(new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                Is2FaEnabled = await _userManager.GetTwoFactorEnabledAsync(user),
                RecoveryCodeleft = await _userManager.CountRecoveryCodesAsync(user)
            });

        }
        [HttpGet]
        public async Task<IActionResult> GenerateRecoveryCode()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            var IsTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (!IsTwoFactorEnabled)
                throw new InvalidOperationException("امکان ایجاد کد بازیابی وجود ندارد چون احراز هویت دو مرحله ای فعال نیست");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GeneratRecoveryCode")]
        public async Task<IActionResult> GenerateRecovery()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            var IsTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (!IsTwoFactorEnabled)
                throw new InvalidOperationException("امکان ایجاد کد بازیابی وجود ندارد چون احراز هویت دو مرحله ای فعال نیست");
            var RecoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            return View("ShowRecoveryCodes", RecoveryCodes.ToArray());
        }
        [HttpGet]
        public async Task<IActionResult> ResetAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ResetAuthenticator")]
        public async Task<IActionResult> ResetAuthenticatorapp()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            await _SignInManager.RefreshSignInAsync(user);
            return RedirectToAction("EnableAuthenticator");
        }

    }
}
