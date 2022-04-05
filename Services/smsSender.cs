using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public class smsSender : IsmsSender
    {
        public async Task<string> SendAuthAsync(string PhoneNumber , string Code)
        {
            HttpClient client = new HttpClient();
            var httpResponse = await client.GetAsync($"");
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                return "Sucsess";
            else
                return "Failed";
        }
    }
}
