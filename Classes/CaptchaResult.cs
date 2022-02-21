using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Classes
{
    public class CaptchaResult
    {
        public string CaptchaCode { get; set; }
        public byte[] CaptchaByteData { get; set; }
        public string CaptchaBase64data => Convert.ToBase64String(CaptchaByteData);
        public DateTime Timestamp { get; set; }

    }
}
