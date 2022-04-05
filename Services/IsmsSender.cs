using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public interface IsmsSender
    {
        Task<string> SendAuthAsync(string PhoneNumber, string Code);
    }
}
