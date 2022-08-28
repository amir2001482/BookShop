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
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UrlEncoder _urlEncoder;
        public UserController(IApplicationUserManager userManager, SignInManager<ApplicationUser> signInManager, UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _urlEncoder = urlEncoder;
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
        public async Task<IActionResult> EnableAuthenticator(EnableAuthenticatorViewModel ViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            if(!ModelState.IsValid)
            {
                return View(await LoadSharedKeyAndQrCodeUriAsync(user));
            }

            var VerificationCode = ViewModel.Code.Replace(" ", string.Empty).Replace("-", string.Empty);
            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, ViewModel.Code);
            if(!is2faTokenValid)
            {
                ModelState.AddModelError(string.Empty, "کد اعتبارسنجی نامعتبر است.");
                return View(await LoadSharedKeyAndQrCodeUriAsync(user));
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);

            if(await _userManager.CountRecoveryCodesAsync(user)==0)
            {
                var RecoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                ViewBag.Alert = "اپلیکشن احراز هویت شما تایید شده است";
                return View("ShowRecoveryCodes",RecoveryCodes);
            }

            else
            {
                return RedirectToAction("TwoFactorAuthentication",new { alert = "success" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication(string alert)
        {
            if(alert!=null)
                ViewBag.Alert = "اپلیکشن احراز هویت شما تایید شده است";

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            TwoFactorAuthenticationViewModel ViewModel = new TwoFactorAuthenticationViewModel
            {
                HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
                RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user),
                Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user),
            };

            return View(ViewModel);
        }


        public async Task<EnableAuthenticatorViewModel> LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user)
        {
            var unFormattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unFormattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unFormattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            EnableAuthenticatorViewModel ViewModel = new EnableAuthenticatorViewModel
            {
                AuthenticatorUri = GenerateQrCodeUri(unFormattedKey, user.Email),
                SharedKey = FormatKey(unFormattedKey),
            };

            return ViewModel;
        }

        public string FormatKey(string unFormattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while(currentPosition+4< unFormattedKey.Length)
            {
                result.Append(unFormattedKey.Substring(currentPosition, 4));
                result.Append(" ");
                currentPosition += 4;
            }

            if(currentPosition<unFormattedKey.Length)
            {
                result.Append(currentPosition);
            }

            return result.ToString().ToLowerInvariant();
        }


        public string GenerateQrCodeUri(string unFormattedKey,string email)
        {
            string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
            return (string.Format(AuthenticatorUriFormat, _urlEncoder.Encode("BookShop"), _urlEncoder.Encode(email), unFormattedKey));

        }

        [HttpGet]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var IsTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (!IsTwoFactorEnabled)
                throw new InvalidOperationException("امکان ایجاد کد بازیابی وجود ندارد چون احراز هویت دو مرحله ای فعال نیست.");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GenerateRecoveryCodes")]
        public async Task<IActionResult> GenerateRecovery()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var IsTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (!IsTwoFactorEnabled)
                throw new InvalidOperationException("امکان ایجاد کد بازیابی وجود ندارد چون احراز هویت دو مرحله ای فعال نیست.");

            var RecoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            return View("ShowRecoveryCodes",RecoveryCodes.ToArray());

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
        public async Task<IActionResult> ResetAuthenticatorApp()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("EnableAuthenticator");
        }
    }
}