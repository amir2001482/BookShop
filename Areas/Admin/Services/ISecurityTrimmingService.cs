using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShop.Areas.Admin.Services
{
    public  interface ISecurityTrimmingService
    {
        bool CanCurrentUserAccess(string area, string controller, string action);
        bool CanUserAccess(ClaimsPrincipal user, string area, string controller, string action);
    }
}
