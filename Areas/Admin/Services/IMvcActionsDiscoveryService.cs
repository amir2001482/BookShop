using BookShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Admin.Services
{
    public interface IMvcActionsDiscoveryService
    {
        ICollection<ControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName);
    }
}
