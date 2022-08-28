using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Services
{
    public interface ISmsSender
    {
        Task<string> SendAuthSmsAsync(string Code, string PhoneNumber);
    }
}
