using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookShop.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BookShopUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public DateTime Register { get; set; }
        public DateTime? LastVisitDateTime { get; set; }
        public bool IsActive { get; set; }
        public List<ApplicationUserRole> userRoles { get; set; }
    }
}
