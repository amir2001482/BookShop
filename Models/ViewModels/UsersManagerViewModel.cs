using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
    public class UsersViewModel
    {
         public string Id { get; set; }

        [Required(ErrorMessage ="وارد نمودن {0} الزامی است.")]
        [Display(Name="نام کاربری")]
        public string UserName { get; set; }
        
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده صحیح نمی باشد.")]
        public string Email { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string LastName { get; set; }

        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string PersianBirthDate { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public string Image { get; set; }

        [Display(Name = "تاریخ عضویت")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "آخرین بازدید")]
        public DateTime? LastVisitDateTime { get; set; }

        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }

        [Display(Name ="نقش ها")]
        public  IEnumerable<string> Roles { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        public bool EmailConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

    }

    public class UserResetPasswordViewModel
    {
        public string Id { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        public string NewPassword { get; set; }
    }

}
