using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Admin.Data;
using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("مدیریت کاربران")]
    public class UsersManagerController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IConvertDate _converDate;
        private readonly IEmailSender _emailSender;
        public UsersManagerController(IApplicationUserManager userManager, IApplicationRoleManager roleManager, IConvertDate convertDate, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _converDate = convertDate;
            _emailSender = emailSender;
        }


        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("مشاهده کاربران")]
        public async Task<IActionResult> Index(string Msg,int page=1,int row=10)
        {
            if(Msg=="Success")
                ViewBag.Alert = "عضویت با موفقیت انجام شد.";

            if (Msg== "SendEmailSuccess")
                ViewBag.Alert = "ارسال ایمیل به کاربران با موفقیت انجام شد.";

            var PagingModel = PagingList.Create(await _userManager.GetAllUsersWithRolesAsync(), row, page);
            return View(PagingModel);
        }


        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("مدیریت کاربر")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();
            else
            {
                var User = await _userManager.FindUserWithRolesByIdAsync(id);
                if (User == null)
                    return NotFound();
                else
                    return View(User);
            }
        }

        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("ویرایش اطلاعات کاربر")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();
            var User =await _userManager.FindUserWithRolesByIdAsync(id);
            if (User == null)
                return NotFound();
            else
            {
                ViewBag.AllRoles = _roleManager.GetAllRoles();
                if(User.BirthDate!=null)
                    User.PersianBirthDate = _converDate.ConvertMiladiToShamsi((DateTime)User.BirthDate, "yyyy/MM/dd");

                return View(User);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                var User = await _userManager.FindByIdAsync(ViewModel.Id);
                if (User == null)
                    return NotFound();
                else
                {
                    IdentityResult Result;
                    var RecentRoles = await _userManager.GetRolesAsync(User);
                    if (ViewModel.Roles == null)
                        ViewModel.Roles = new string[] { };

                    var DeleteRoles = RecentRoles.Except(ViewModel.Roles);
                    var AddRoles = ViewModel.Roles.Except(RecentRoles);

                    Result= await _userManager.RemoveFromRolesAsync(User,DeleteRoles);
                    if(Result.Succeeded)
                    {
                        Result = await _userManager.AddToRolesAsync(User, AddRoles);
                        if(Result.Succeeded)
                        {
                            User.FirstName = ViewModel.FirstName;
                            User.LastName = ViewModel.LastName;
                            User.Email = ViewModel.Email;
                            User.PhoneNumber = ViewModel.PhoneNumber;
                            User.UserName = ViewModel.UserName;
                            User.BirthDate = _converDate.ConvertShamsiToMiladi(ViewModel.PersianBirthDate);
                            
                            await _userManager.UpdateSecurityStampAsync(User);

                            Result = await _userManager.UpdateAsync(User);
                            if(Result.Succeeded)
                            {
                                ViewBag.AlertSuccess = "ذخیره تغییرات با موفقیت انجام شد.";
                            }
                        }
                    }

                    if(Result!=null)
                    {
                        foreach(var item in Result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }

            ViewBag.AllRoles = _roleManager.GetAllRoles();
            return View(ViewModel);
        }

        [HttpGet]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("حذف کاربر")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
                return NotFound();
            else
                return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> Deleted(string id)
        {
            if (id == null)
                return NotFound();
            var User = await _userManager.FindByIdAsync(id);
            if (User == null)
                return NotFound();
            else
            {
                var Result=await _userManager.DeleteAsync(User);
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                else
                    ViewBag.AlertError = "در حذف اطلاعات خطایی رخ داده است.";

                return View(User);
            }
        }

        public async Task<IActionResult> SendEmail(string[] emails,string subject,string message)
        {
            if(emails!=null)
            {
                for(int i=0;i<emails.Length;i++)
                {
                   await _emailSender.SendEmailAsync(emails[i], subject, message);
                }
            }

            return RedirectToAction("Index", new { Msg = "SendEmailSuccess" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeLockOutEnable(string UserId,bool Status)
        {
            var User = await _userManager.FindByIdAsync(UserId);
            if(User==null)
            {
                return NotFound();
            }
            else
            {
                await _userManager.SetLockoutEnabledAsync(User, Status);
                return RedirectToAction("Details", new { id = UserId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUserAccount(string UserId)
        {
            var User = await _userManager.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(User, DateTimeOffset.UtcNow.AddMinutes(20));
                return RedirectToAction("Details", new { id = UserId });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnLockUserAccount(string UserId)
        {
            var User = await _userManager.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(User, null);
                return RedirectToAction("Details", new { id = UserId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InActiveOrActiveUser(string UserId,bool Status)
        {
            var User = await _userManager.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }

            User.IsActive = Status;
            await _userManager.UpdateAsync(User);
            return RedirectToAction("Details", new { id = UserId });
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string UserId)
        {
            var User = await _userManager.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }

            var ViewModel = new UserResetPasswordViewModel
            {
                Id = UserId,
                UserName = User.UserName,
                Email = User.Email,
            };

            return View(ViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                var User = await _userManager.FindByIdAsync(ViewModel.Id);
                if (User == null)
                {
                    return NotFound();
                }

                await _userManager.RemovePasswordAsync(User);
                await _userManager.AddPasswordAsync(User, ViewModel.NewPassword);
                ViewBag.AlertSuccess = "بازنشانی کلمه عبور با موفقیت انجام شد.";
                ViewModel.UserName = User.UserName;
                ViewModel.Email = User.Email;
            }

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTwoFactorEnabled(string UserId)
        {
            var User = await _userManager.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }

            if (User.TwoFactorEnabled)
                User.TwoFactorEnabled = false;
            else
                User.TwoFactorEnabled = true;

            var Result = await _userManager.UpdateAsync(User);
            if(!Result.Succeeded)
            {
                foreach(var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Details", new { id = UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmailConfirmed(string UserId)
        {
            var User = await _userManager.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }

            if (User.EmailConfirmed)
                User.EmailConfirmed = false;
            else
                User.EmailConfirmed = true;

            var Result = await _userManager.UpdateAsync(User);
            if (!Result.Succeeded)
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Details", new { id = UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePhoneNumberConfirmed(string UserId)
        {
            var User = await _userManager.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }

            if (User.PhoneNumberConfirmed)
                User.PhoneNumberConfirmed = false;
            else
                User.PhoneNumberConfirmed = true;

            var Result = await _userManager.UpdateAsync(User);
            if (!Result.Succeeded)
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("Details", new { id = UserId });

        }

        public async Task<IActionResult> GetUsersInRole(string id,int page=1,int row=10)
        {
            var PagingModel = PagingList.Create(await _roleManager.GetUsersInRoleAsync(id), row, page);
            return View("Index", PagingModel);
        }

    }
}