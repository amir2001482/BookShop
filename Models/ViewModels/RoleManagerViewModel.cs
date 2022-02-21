using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
    public class RoleManagerViewModel
    {
        public string Id { get; set; }
        [Display(Name ="نام نقش")]
        [Required(ErrorMessage ="وارد کردن {0} الزامی است  ")]
        public string RoleName { get; set; }
        [Display(Name ="توضیحات")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی است  ")]
        public string Description { get; set; }
        [Display(Name = "تعداد کاربران")]
        public int UsersCount { get; set; }
    }
}
