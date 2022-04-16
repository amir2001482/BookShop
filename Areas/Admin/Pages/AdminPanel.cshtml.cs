using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookShop.Areas.Admin.Pages
{
    public class AdminPanelModel : PageModel
    {
        private readonly IApplicationUserManager _userManager;
        public AdminPanelModel(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public class InputModel
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
        }
        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            
            return Page();
        }
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Admin/Dushbord/Index?MSG=Suc");
            if (!ModelState.IsValid)
                return NotFound();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();
            var Result = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (Result.Succeeded)
                return LocalRedirect(returnUrl);
            foreach (var erorr in Result.Errors)
                ModelState.AddModelError(string.Empty, erorr.Description);
            return Page();
            
        }
    }
}
