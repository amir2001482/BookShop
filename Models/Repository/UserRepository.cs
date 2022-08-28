using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IConvertDate _convertDate;
        private readonly IApplicationRoleManager _roleManager;

        public UserRepository(IApplicationUserManager userManager, IConvertDate convertDate, IApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _convertDate = convertDate;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterBaseViewModel viewModel)
        {

            DateTime BirthDateMiladi = _convertDate.ConvertShamsiToMiladi(viewModel.BirthDate);
            var user = new ApplicationUser { UserName = viewModel.UserName, Email = viewModel.Email, PhoneNumber = viewModel.PhoneNumber, RegisterDate = DateTime.Now, IsActive = true, BirthDate = BirthDateMiladi , EmailConfirmed = viewModel.EmailConfirmed , PhoneNumberConfirmed = viewModel.PhoneNumberConfirmed };
            IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
                return await AddRoleAsync("کاربر", user);
            return result;
        }

        public async Task<IdentityResult> AddRoleAsync(string name , ApplicationUser user)
        {
            var role = _roleManager.FindByNameAsync(name);
            if (role == null)
                await _roleManager.CreateAsync(new ApplicationRole(name));

             return await _userManager.AddToRoleAsync(user, name);
        }
    }
}
