using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Data
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole> , IApplicationRoleManager
    {
        private readonly IdentityErrorDescriber _errors;
        private readonly IApplicationUserManager _userManager;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationRoleManager> _logger;
        private readonly IEnumerable<IRoleValidator<ApplicationRole>> _roleValidators;
        private readonly IRoleStore<ApplicationRole> _store;

        public ApplicationRoleManager(
            IRoleStore<ApplicationRole> store,
            ILookupNormalizer keyNormalizer,
            ILogger<ApplicationRoleManager> logger,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            IdentityErrorDescriber errors,
            IApplicationUserManager userManager) :
            base(store, roleValidators, keyNormalizer, errors, logger)
        {
            _errors = errors;
            _keyNormalizer = keyNormalizer;
            _logger = logger;
            _store = store;
            _roleValidators = roleValidators;
            _userManager = userManager;
        }


        public List<ApplicationRole> GetAllRoles()
        {
            return Roles.ToList();
        }


        public List<RolesViewModel> GetAllRolesAndUsersCount()
        {
            return Roles.Select(role =>
                             new RolesViewModel
                             {
                                 RoleID = role.Id,
                                 RoleName = role.Name,
                                 Description = role.Description,
                                 UsersCount = role.Users.Count(),
                             }).ToList();
        }

        public Task<ApplicationRole> FindClaimsInRole(string RoleID)
        {
            return Roles.Include(c => c.Claims).FirstOrDefaultAsync(c => c.Id == RoleID);
        }


        public async Task<IdentityResult> AddOrUpdateClaimsAsync(string RoleID,string RoleClaimType,IList<string> SelectedRoleClaimValues)
        {
            var Role = await FindClaimsInRole(RoleID);
            if(Role==null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "NotFound",
                    Description = "نقش مورد نظر یافت نشد.",
                });
            }

            var CurrentRoleClaimValues = Role.Claims.Where(r => r.ClaimType == RoleClaimType).Select(r => r.ClaimValue).ToList();
            if (CurrentRoleClaimValues == null)
                CurrentRoleClaimValues = new List<string>();

            var NewClaimValuesToAdd = SelectedRoleClaimValues.Except(CurrentRoleClaimValues).ToList();
            foreach(var claim in NewClaimValuesToAdd)
            {
                Role.Claims.Add(new ApplicationRoleClaim
                {
                    RoleId=RoleID,
                    ClaimType=RoleClaimType,
                    ClaimValue=claim,
                });
            }

            var RemovedClaimValues = CurrentRoleClaimValues.Except(SelectedRoleClaimValues).ToList();
            foreach(var claim in RemovedClaimValues)
            {
                var RoleClaim = Role.Claims.SingleOrDefault(r => r.ClaimValue == claim && r.ClaimType == RoleClaimType);
                if (RoleClaim != null)
                    Role.Claims.Remove(RoleClaim);
            }

            return await UpdateAsync(Role);
        }

        public async Task<List<UsersViewModel>> GetUsersInRoleAsync(string RoleID)
        {
            var UserIds = (from r in Roles
                           where (r.Id == RoleID)
                           from u in r.Users
                           select u.UserId).ToList();

            return await _userManager.Users.Where(user => UserIds.Contains(user.Id))
                .Select(user => new UsersViewModel
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
                }).AsNoTracking().ToListAsync();
        }
    }
}
