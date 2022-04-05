using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.ViewModels;
using BookShop.Services;
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
        private readonly IsmsSender _smsSender;

        public AccountController(IApplicationRoleManager roleManager, IApplicationUserManager userManager, IEmailSender emailSender, SignInManager<ApplicationUser> signInManager , IsmsSender smsSender)
        {
            _roleManager = roleManager;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _smsSender = smsSender;
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
                IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByNameAsync("کاربر");
                    if (role == null)
                        await _roleManager.CreateAsync(new ApplicationRoles("کاربر"));
                    result = await _userManager.AddToRoleAsync(user, "کاربر");
                    if (result.Succeeded)
                    {
                        var Code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
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
        public async Task<IActionResult> ConfirmEmail(string Id, string Code)
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
                if (Captcha.ValidateCaptchaCode(VM.CaptchaCode, HttpContext))
                {
                    var user = await _userManager.FindByNameAsync(VM.UserName);
                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            var result = await _signInManager.PasswordSignInAsync(VM.UserName, VM.Password, VM.RememberMe, true);
                            if (result.Succeeded)
                            {
                                if (result.IsLockedOut)
                                {
                                    ModelState.AddModelError(string.Empty, "حساب کاربری شما به مدت 20 دقیقه به دلیل تلاش های ناموفق قفل شد. ");
                                    return View();
                                }
                                else
                                    return RedirectToAction("Index", "Home");
                                
                            }
                            ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور شما صحیح نمی باشد");
                        }
                        else
                            ModelState.AddModelError(string.Empty, "کلمه امنیتی صحیح نمی باشد");
                    }
                }
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
        [HttpGet]
        public IActionResult ForgetPassWord()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassWord(ForgetPassword VM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(VM.Email);
                if (user == null)
                    ModelState.AddModelError(string.Empty, "ایمیل شما تایید نشده است");
                else
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                        ModelState.AddModelError(string.Empty, "ایمیل شما تایید نشده است");
                    else
                    {
                        var Code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var CallBackUrl = Url.Action("ResetPassword", "Account", values: new { user.Id, Code }, protocol: Request.Scheme);
                        await _emailSender.SendEmailAsync(user.Email, "بازیابی رمز عبور", $"<p style='font-family:tahoma;font-size:14px'> برای بازنشانی کلمه عبور خود <a href='{HtmlEncoder.Default.Encode(CallBackUrl)}'>اینجا کلیک کنید</a> </p>");
                        return RedirectToAction("ForgetPasswordConfirmation");
                    }
                }
            }
            return View(VM);
        }
        [HttpGet]
        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
                return NotFound();
            else
            {
                var VM = new ResetPasswordViewModel { Code = code };
                return View(VM);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel VM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(VM.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "ایمیل شما صحیح نمی باشد");
                }
                else
                {
                    var result = await _userManager.ResetPasswordAsync(user, VM.Code, VM.Password);
                    if (result.Succeeded)
                        return RedirectToAction("resetPasswordConfirmation");
                    else
                    {
                        foreach (var item in result.Errors)
                            ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View(VM);
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        public async Task<IActionResult> SendSMS()
        {
            var Result = await _smsSender.SendAuthAsync("09398730453", "8585");
            if (Result == "Sucsess")
                ViewBag.MSG = "ارسال پیامک با موفقیت انجام شد";
            else
                ViewBag.MSG = "خطایی رخ داده است ";
            return View();



        }




    }
}
