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
    public class ApplicationUserManager : UserManager<ApplicationUser>,IApplicationUserManager
    {
        private readonly ApplicationIdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationUserManager> _logger;
        private readonly IOptions<IdentityOptions> _options;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IEnumerable<IPasswordValidator<ApplicationUser>> _passwordValidators;
        private readonly IServiceProvider _services;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IEnumerable<IUserValidator<ApplicationUser>> _userValidators;

        public ApplicationUserManager(
            ApplicationIdentityErrorDescriber errors,
            ILookupNormalizer keyNormalizer,
            ILogger<ApplicationUserManager> logger,
            IOptions<IdentityOptions> options,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            IServiceProvider services,
            IUserStore<ApplicationUser> userStore,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators)
            : base(userStore,options,passwordHasher,userValidators,passwordValidators,keyNormalizer,errors,services,logger)
        {
            _userStore = userStore;
            _errors = errors;
            _logger = logger;
            _services = services;
            _passwordHasher = passwordHasher;
            _userValidators = userValidators;
            _options = options;
            _keyNormalizer = keyNormalizer;
            _passwordValidators = passwordValidators;
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<List<UsersViewModel>> GetAllUsersWithRolesAsync()
        {
            return await Users.Select(user => new UsersViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive,
                LastVisitDateTime = user.LastVisitDateTime,
                Image = user.Image,
                RegisterDate = user.RegisterDate,
                Roles=user.Roles.Select(u=>u.Role.Name),

            }).ToListAsync();
        }

        public async Task<UsersViewModel> FindUserWithRolesByIdAsync(string UserID)
        {
            return await Users.Where(u => u.Id == UserID).Select(user => new UsersViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive,
                LastVisitDateTime = user.LastVisitDateTime,
                Image = user.Image,
                RegisterDate = user.RegisterDate,
                Roles = user.Roles.Select(u => u.Role.Name),
                AccessFailedCount = user.AccessFailedCount,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,

            }).FirstOrDefaultAsync();
        }

        public async Task<string> GetFullName(ClaimsPrincipal User)
        {
            var UserInfo = await GetUserAsync(User);
            return UserInfo.FirstName + " " + UserInfo.LastName;
        }
    }
}
