using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagerController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IConvertData _convertData;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IEmailSender _emailSender;
        public UserManagerController(IApplicationUserManager userManager, IConvertData convertData, IApplicationRoleManager roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _convertData = convertData;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index(string MSG, int page = 1, int row = 10)
        {
            var PagingModel = PagingList.Create(await _userManager.GetAllUsersWithRolesAsync(), row, page);
            if (MSG == "Sucsess")
                ViewBag.Alert = "عملیات با موفقیت انجام گرفت";

            if (MSG == "SendEmailSuccses")
                ViewBag.Alert = "ایمیل با موفقیت به کاربران ارسال شد";
            return View(PagingModel);
        }
        public async Task<IActionResult> Details(string Id, int page = 1, int row = 10)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var User = await _userManager.FindUserByIdWithRolesAsync(Id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var User = await _userManager.FindUserByIdWithRolesAsync(Id);
            if (User == null)
            {
                return NotFound();
            }
            User.PersianBirthDate = _convertData.ConvertMiladiToShamsi(User.BirthDate, "yyyy/MM/dd");
            ViewBag.AllRoles = _roleManager.GetAllRoles();

            return View(User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersManagerViewModel VM)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByIdAsync(VM.Id);
                if (User == null)
                    return NotFound();
                IdentityResult Result;
                var RecentRoles = await _userManager.GetRolesAsync(User);
                var DeleteRoles = RecentRoles.Except(VM.Roles);
                var AddRoles = VM.Roles.Except(RecentRoles);
                Result = await _userManager.RemoveFromRolesAsync(User, DeleteRoles);
                if (Result.Succeeded)
                {
                    Result = await _userManager.AddToRolesAsync(User, AddRoles);
                    if (Result.Succeeded)
                    {
                        User.BirthDate = _convertData.ConvertShamsiToMiladi(VM.PersianBirthDate);
                        User.Email = VM.Email;
                        User.FirstName = VM.FirstName;
                        User.Id = VM.Id;
                        User.Image = VM.Image;
                        User.LastName = VM.LastName;
                        User.PhoneNumber = VM.PhoneNumber;
                        User.UserName = VM.UserName;

                        Result = await _userManager.UpdateAsync(User);
                        if (Result.Succeeded)
                        {
                            ViewBag.AlertSuccess = "ذخیره تغییرات با موفقیت انجام شد.";
                        }
                    }
                }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            ViewBag.AllRoles = _roleManager.GetAllRoles();
            return View(VM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id == null)
                return NotFound();
            var User = await _userManager.FindByIdAsync(Id);
            if (User == null)
                return NotFound();

            return View(User);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleted(string Id)
        {
            var User = await _userManager.FindByIdAsync(Id);
            var Result = await _userManager.DeleteAsync(User);
            if (Result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AlertError = "در حذف اطلاعات خطایی رخ داده است.";
                return View(User);
            }

        }
        public async Task<IActionResult> SendEmail(string[] emails, string subject, string massege)
        {
            if (emails != null)
            {
                for (int i = 0; i < emails.Length; i++)
                {
                    await _emailSender.SendEmailAsync(emails[i], subject, massege);
                }
                return RedirectToAction("Index", new { MSG = "SendEmailSuccses" });
            }
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeLockOutEnd(string userId, bool status)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            else
            {
                var result = await _userManager.SetLockoutEnabledAsync(user, status);
                return RedirectToAction("Details", new { Id = userId });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUserAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            else
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));
                return RedirectToAction("Details", new { Id = userId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnLockUserAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            else
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, null);
                return RedirectToAction("Details", new { Id = userId });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InActiveOrActive(string userId, bool status)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            user.IsActive = status;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Details", new { Id = userId });
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            UserResetPasswordViewModel VM = new UserResetPasswordViewModel
            {
                Email = user.Email,
                Id = user.Id,
                UserName = user.UserName
            };
            return View(VM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel VM)
        {  
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(VM.Id);
                if (user == null)
                    return NotFound();
                await _userManager.RemovePasswordAsync(user);
                var result = await _userManager.AddPasswordAsync(user, VM.NewPassword);
                if (result.Succeeded)
                    ViewBag.MSG = "کلمه عبور با موفقیت تغییر کرد";
                else
                    ModelState.AddModelError(string.Empty, "خطایی رخ داده است لطفا دوباره امتحان کنید");
                return View(VM);
            }
            else
            {
                ViewBag.MSG = "اطلاعات فرم نا معتبر است ";
                return View(VM);
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTwoFactorEnabled(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return NotFound();
            var HasTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (HasTwoFactorEnabled)
                user.TwoFactorEnabled = false;
            else
                user.TwoFactorEnabled = true;
            var Result =  await _userManager.UpdateAsync(user);
            if (!Result.Succeeded)
            {
                foreach (var erorr in Result.Errors)
                    ModelState.AddModelError(string.Empty, erorr.Description);
            }
            return RedirectToAction("Details", new { Id = Id });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmailConfirmed(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return NotFound();
            var IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (IsEmailConfirmed)
                user.EmailConfirmed = false;
            else
                user.EmailConfirmed = true;
            var Result = await _userManager.UpdateAsync(user);
            if(!Result.Succeeded)
                foreach(var erorr in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, erorr.Description);
                }
            return RedirectToAction("Details", new { Id = Id });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePhoneConfirmed(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return NotFound();
            var IsPhoneConfirmed = await _userManager.IsPhoneNumberConfirmedAsync(user);
            if (IsPhoneConfirmed)
                user.PhoneNumberConfirmed = false;
            else
                user.PhoneNumberConfirmed = true;
            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded)
                foreach (var erorr in Result.Errors)
                    ModelState.AddModelError(string.Empty, erorr.Description);
            return RedirectToAction("Details", new { Id = Id });
        }
    }
}
