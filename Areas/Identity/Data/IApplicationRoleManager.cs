using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Data
{
    public interface IApplicationRoleManager
    {
        #region baseClass
        IQueryable<ApplicationRoles> Roles { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IList<IRoleValidator<ApplicationRoles>> RoleValidators { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }
        Task<IdentityResult> CreateAsync(ApplicationRoles role);
        Task<IdentityResult> DeleteAsync(ApplicationRoles role);
        Task<ApplicationRoles> FindByIdAsync(string roleId);
        Task<ApplicationRoles> FindByNameAsync(string roleName);
        string NormalizeKey(string key);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> UpdateAsync(ApplicationRoles role);
        Task UpdateNormalizedRoleNameAsync(ApplicationRoles role);
        Task<string> GetRoleNameAsync(ApplicationRoles role);
        Task<IdentityResult> SetRoleNameAsync(ApplicationRoles role, string name);

        #endregion


        #region CustomMethod
        List<ApplicationRoles> GetAllRoles();
        List<RoleManagerViewModel> GetAllRolesAndUsersCount();
        #endregion
    }
}
