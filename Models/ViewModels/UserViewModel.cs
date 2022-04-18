using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
   public class EnableAuthenticatorViewModel
   {
        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [StringLength(7, ErrorMessage = "کد اعتبارسنجی باید حداقل دارای {2} کاراکتر و حداکثر دارای {1} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "کد اعتبارسنجی")]
        public string Code { get; set; }
        public string SharedKey { get; set; }
        public string AuthenticatorUri { get; set; }
   }
   public class TwoFactorAuthenticationViewModel
    {
        public bool Is2FaEnabled { get; set; }
        public bool HasAuthenticator { get; set; }
        public int RecoveryCodeleft { get; set; }
    }
}
