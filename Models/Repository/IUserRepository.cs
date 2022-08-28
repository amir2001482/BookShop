using BookShop.Areas.Identity.Data;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterBaseViewModel viewModel);
        Task<IdentityResult> AddRoleAsync(string name, ApplicationUser user);

    }
}
