using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{

    public class RegisterViewModel : GoogleRecaptchaModelBase
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [EmailAddress(ErrorMessage = "ایمیل شما نامعتبر است.")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "کلمه عبور وارد شده با تکرار کلمه عبور مطابقت ندارد.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string UserName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string PhoneNumber { get; set; }
    }
    public class SignInViewModel
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMe { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "کلمه امنیتی")]
        public string CaptchaCode { get; set; }

    }
    public class ForgetPassword
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = " ایمیل")]
        public string Email { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = " ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "کلمه عبور جدید")]
        [StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "تکرار کلمه عبور جدید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور وارد شده مطابقت ندارد.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = " ایمیل")]
        public string Code { get; set; }
    }
    public class SendCodeViewModel
    {
        public ICollection<SelectListItem> Providers { get; set; }
        public bool RememberMe { get; set; }
        public string SelectedProvider { get; set; }
    }
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "کد اعتبارسنجی")]
        public string Code { get; set; }
        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMeBrowser { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [StringLength(100, ErrorMessage = "{0} باید دارای حداقل {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور جدید")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور وارد شده با تکرار کلمه عبور مطابقت ندارد.")]
        [Display(Name = "تکرار کلمه عبور جدید")]
        public string ConfirmPassword { get; set; }
        public UserSideBarVieModel SideBar { get; set; }
    }
    public class UserSideBarVieModel
    {
        public string FullName { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? LastVisit { get; set; }
        public string Image { get; set; }
    }
    public class LoginWith2FaViewModel
    {
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [StringLength(7, ErrorMessage = "کد اعتبارسنجی با حداقل دارای {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "کد اعتبارسنجی")]
        public string TwoFactorCode { get; set; }
        public bool RememberMe { get; set; }
        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMachine { get; set; }
    }


}
