using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationRoleManager _roleManager;
        private readonly IApplicationUserManager _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IApplicationRoleManager roleManager, IApplicationUserManager userManager, IEmailSender emailSender , SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
       {
            return View();
       }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistarViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    UserName = viewModel.UserName,
                    Register = DateTime.Now,
                    IsActive = true
                };
                IdentityResult result = await _userManager.CreateAsync(user , viewModel.Password);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync("کاربر");
                    if (role == null)
                        await _roleManager.CreateAsync(new ApplicationRoles("کاربر"));
                    result = await _userManager.AddToRoleAsync(user, "کاربر");
                    if (result.Succeeded)
                    {
                        var Code =  await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callBackUrl = Url.Action("ConfirmEmail", "Account", values: new { user.Id, Code }, protocol: Request.Scheme);
                        await _emailSender.SendEmailAsync(user.Email, "تایید ایمیل حساب کاربری سایت پنجره نو", $"<div dir='rtl' style='font-family:tahoma;font-size:14px'>لطفا با کلیک روی لینک رویه رو ایمیل خود را تایید کنید.  <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>کلیک کنید</a></div>");
                        return RedirectToAction("Index", "Home", new { Id = "ConfirmedEmail" });
                    }

                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();   
            }
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string Id , string Code)
        {
            if (Id == null || Code == null)
                return RedirectToAction("Index", "Home");
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return NotFound("چنین کابری یافت نشد");
            var result = await _userManager.ConfirmEmailAsync(user, Code);
            if (!result.Succeeded)
                throw new InvalidOperationException($"Error Confirming email for user with ID '{Id}'");
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel VM)
        {
            if (ModelState.IsValid)
            {
                if(Captcha.ValidateCaptchaCode(VM.CaptchaCode , HttpContext))
                {
                    var result = await _signInManager.PasswordSignInAsync(VM.UserName, VM.Password, VM.RememberMe, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور شما صحیح نمی باشد");
                }
                ModelState.AddModelError(string.Empty, "کلمه امنیتی صحیح نمی باشد");
            }
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
     
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }

    }
}
