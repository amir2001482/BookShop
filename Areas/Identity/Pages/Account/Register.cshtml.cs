using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BookShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookShop.Classes;

namespace BookShop.Areas.Identity.Pages.Account
{
    [Area("Admin")]
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IApplicationRoleManager _roleManager;
        private readonly ConvertData _convertData;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger,
            IApplicationRoleManager roleManager,
            ConvertData convertData)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            _convertData = convertData;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public List<ApplicationRoles> Roles { get; set; }
        [BindProperty]
        public string[] userRoles { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "وارد کردن {0} الزامی است .")]
            [Display(Name = "نام ")]
            public string Name { get; set; }
            [Required(ErrorMessage = "وارد کردن {0} الزامی است .")]
            [Display(Name = "نام خانوادگی")]
            public string Family { get; set; }
            [Required(ErrorMessage = "وارد کردن {0} الزامی است .")]
            [Display(Name = "نام کاربری")]
            public string userName { get; set; }
            [Required(ErrorMessage = "وارد کردن {0} الزامی است .")]
            [Display(Name = "تاریخ تولد ")]
            public string BirthDate { get; set; }
            [Required(ErrorMessage = "وارد کردن {0} الزامی است .")]
            [Display(Name = "شماره تلفن ")]
            public string PhoneNumber { get; set; }
            [Required(ErrorMessage = "وارد کردن {0} الزامی است .")]
            [EmailAddress]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required(ErrorMessage = "وارد کردن {0} الزامی است .")]
            [StringLength(100, ErrorMessage = "{0} باید حد اقل دارای {2} کاراکتر و حداکثر {1} کاراکتر باشد .", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "کلمه عبور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تایید کلمه عبور")]
            [Compare("Password", ErrorMessage = "کلمه عبور و تایید آن مغایرت دارد ")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            var roles = _roleManager.GetAllRoles();
            Roles = roles;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Admin/UserManager/Index?MSG=Sucsess");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.userName,
                    Email = Input.Email,
                    BirthDate = _convertData.ConvertShamsiToMiladi(Input.BirthDate),
                    FirstName = Input.Name,
                    LastName = Input.Family,
                    PhoneNumber = Input.PhoneNumber,
                    IsActive = true,
                    Register = DateTime.Now,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    if (userRoles != null)
                    {
                        await _userManager.AddToRolesAsync(user, userRoles);
                    }

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                var roles = _roleManager.GetAllRoles();
                Roles = roles;
                return Page();     
            }
            else
            {
                var roles = _roleManager.GetAllRoles();
                Roles = roles;
                return Page();
            }
           
        }
    }
}
