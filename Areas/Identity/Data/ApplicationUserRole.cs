using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Data
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationRole Role { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
