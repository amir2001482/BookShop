using BookShop.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Services
{
    public interface IJwtService
    {
        Task<string> GeneratTokenAsync(ApplicationUser user);
        Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user);

    }
}
