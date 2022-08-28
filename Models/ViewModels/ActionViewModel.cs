using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
    public class ActionViewModel
    {
        public IList<Attribute> ActionAttributes { get; set; }
        public string ActionDisplayName { get; set; }
        public string ActionId => $"{ControllerId}:{ActionName}";
        public string ActionName { get; set; }
        public string ControllerId { get; set; }
        public bool IsSecuredAction { get; set; }
    }
}
