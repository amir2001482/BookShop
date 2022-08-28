using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Services
{
    public class SmsSender : ISmsSender
    {
        public async Task<string> SendAuthSmsAsync(string Code,string PhoneNumber)
        {
            HttpClient httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync($"https://api.kavenegar.com/v1/989898989898989898/verify/lookup.json?receptor={PhoneNumber}&token={Code}&template=AuthVerify");
            if (httpResponse.StatusCode == HttpStatusCode.OK)
                return "Success";
            else
                return "Failed";
        }
    }
}
