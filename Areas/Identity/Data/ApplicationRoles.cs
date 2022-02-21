using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Data
{
    public class ApplicationRoles : IdentityRole
    {
        public ApplicationRoles()
        {

        }
        public ApplicationRoles(string name) : base(name)
        {

        }
        public ApplicationRoles(string name , string description):base(name)
        {
            Description = description;
        }
        public string Description { get; set; }
        public virtual List<ApplicationUserRole> UserRoles { get; set; } 
    }
}
