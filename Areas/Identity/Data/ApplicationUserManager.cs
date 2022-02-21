using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Data
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
    {
        private readonly ApplicationIdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _normalizer;
        private readonly ILogger<ApplicationUserManager> _logger;
        private readonly IOptions<IdentityOptions> _options;
        private readonly IPasswordHasher<ApplicationUser> _Password;
        private readonly IServiceProvider _Service;
        private readonly IUserStore<ApplicationUser> _store;
        private readonly IEnumerable<IPasswordValidator<ApplicationUser>> _passwordvalidators;
        private readonly IEnumerable<IUserValidator<ApplicationUser>> _userValidators;
        public ApplicationUserManager
            (
                ApplicationIdentityErrorDescriber errors,
                 ILookupNormalizer normalizer ,
                 ILogger<ApplicationUserManager> logger ,
                 IOptions<IdentityOptions> options,
                 IPasswordHasher<ApplicationUser> Password,
                 IServiceProvider Service,
                 IUserStore<ApplicationUser> store,
                 IEnumerable<IPasswordValidator<ApplicationUser>> passwordvalidators,
                 IEnumerable<IUserValidator<ApplicationUser>> userValidators
            ):base(store , options , Password, userValidators, passwordvalidators, normalizer, errors, Service, logger)
        {
            _errors = errors;
            _logger = logger;
            _normalizer = normalizer;
            _options = options;
            _Password = Password;
            _passwordvalidators = passwordvalidators;
            _Service = Service;
            _store = store;
            _userValidators = userValidators;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }
        public async Task<List<UsersManagerViewModel>> GetAllUsersWithRolesAsync()
        {
            return await Users.Select(user => new UsersManagerViewModel
            {
                BirthDate = user.BirthDate,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FirstName = user.FirstName,
                Id = user.Id,
                Image = user.Image,
                IsActive = user.IsActive,
                LastName = user.LastName,
                LastVisitDateTime = user.LastVisitDateTime,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                RegisterDate = user.Register,
                UserName = user.UserName,
                Roles = user.userRoles.Select(u => u.Role.Name),
            }).ToListAsync();
        }
        public async Task<UsersManagerViewModel> FindUserByIdWithRolesAsync(string Id)
        {
            return await Users.Where(u => u.Id == Id).Select(user => new UsersManagerViewModel
            {
                BirthDate = user.BirthDate,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                FirstName = user.FirstName,
                Id = user.Id,
                Image = user.Image,
                IsActive = user.IsActive,
                LastName = user.LastName,
                LastVisitDateTime = user.LastVisitDateTime,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                RegisterDate = user.Register,
                UserName = user.UserName,
                AccessFailedCount = user.AccessFailedCount,
                Roles = user.userRoles.Select(u => u.Role.Name),
                TwoFactorEnabled = user.TwoFactorEnabled
            }).FirstOrDefaultAsync();
        }
        public async Task<string> GetFullName(ClaimsPrincipal UserClaim )
        {
            var UserInfo = await GetUserAsync(UserClaim);
            return UserInfo.FirstName + " " + UserInfo.LastName;
        }
    }
}
