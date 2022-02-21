using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Data
{
    public class ApplicationRoleManager : RoleManager<ApplicationRoles> , IApplicationRoleManager
    {
        private readonly IdentityErrorDescriber _error;
        private readonly ILookupNormalizer _keyNormalize;
        private readonly IEnumerable<IRoleValidator<ApplicationRoles>> _roleValidators;
        private readonly ILogger<ApplicationRoleManager> _logger;
        private readonly IRoleStore<ApplicationRoles> _roleStore;
        public ApplicationRoleManager
            (
                IdentityErrorDescriber error,
                ILookupNormalizer keyNormalize,
                IEnumerable<IRoleValidator<ApplicationRoles>> roleValidators,
                ILogger<ApplicationRoleManager> logger,
                IRoleStore<ApplicationRoles> roleStore
            ) : base(roleStore, roleValidators, keyNormalize, error, logger)
        {
            _error = error;
            _logger = logger;
            _keyNormalize = keyNormalize;
            _roleStore = roleStore;
            _roleValidators = roleValidators;

        }

        public List<ApplicationRoles> GetAllRoles()
        {
            return Roles.ToList();
        }
        public List<RoleManagerViewModel> GetAllRolesAndUsersCount()
        {
            return Roles.Select(r => new RoleManagerViewModel
            {
                Id = r.Id,
                RoleName = r.Name,
                Description = r.Description,
                UsersCount = r.UserRoles.Count()
            }).ToList();
        }
    }
}
