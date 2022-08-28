using BookShop.Areas.Admin.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShop.Areas.Admin.Services
{
    public class SecurityTrimmingService : ISecurityTrimmingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;

        public SecurityTrimmingService(
            IHttpContextAccessor httpContextAccessor,
            IMvcActionsDiscoveryService mvcActionsDiscoveryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
        }

        public bool CanCurrentUserAccess(string area, string controller, string action)
        {
            return _httpContextAccessor.HttpContext != null && CanUserAccess(_httpContextAccessor.HttpContext.User, area, controller, action);
        }

        public bool CanUserAccess(ClaimsPrincipal user, string area, string controller, string action)
        {
            var currentClaimValue = $"{area}:{controller}:{action}";
            var securedControllerActions = _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            if (!securedControllerActions.SelectMany(x => x.MvcActions).Any(x => x.ActionId == currentClaimValue))
            {
                throw new KeyNotFoundException($@"The `secured` area={area}/controller={controller}/action={action} with `ConstantPolicies.DynamicPermission` policy not found. Please check you have entered the area/controller/action names correctly and also it's decorated with the correct security policy.");
            }

            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            return user.HasClaim(claim => claim.Type == ConstantPolicies.DynamicPermissionClaimType &&
                                          claim.Value == currentClaimValue);
        }

    }
}
