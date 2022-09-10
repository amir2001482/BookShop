using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Classes
{
    public class SiteSettings
    {
        public JwtSettings jwtSettings { get; set; }
    }

    public class JwtSettings
    {
        public string Secretkey { get; set; }
        public string EncryptKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }

    }
}
