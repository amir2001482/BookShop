using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Data
{
    public class ApplicationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName) => new IdentityError {Code=nameof(DuplicateUserName),Description=$"نام کاربری '{userName}' قبلا توسط شخص دیگری انتخاب شده است."};

        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError {Code=nameof(PasswordRequiresNonAlphanumeric),Description="کلمه عبور باید حداقل شامل یک کاراکتر غیرعددی و غیر حرفی باشد.(@,%,#,...)"};

        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "کلمه عبور باید حداقل شامل یک عدد (0-9) باشد." };

        public override IdentityError PasswordRequiresLower() => new IdentityError {Code=nameof(PasswordRequiresLower),Description="کلمه عبور باید حداقل شامل یک حرف  کوچک (a-z) باشد."};

        public override IdentityError PasswordRequiresUpper() => new IdentityError {Code=nameof(PasswordRequiresUpper),Description="کلمه عبور باید حداقل شامل یک حرف بزرگ (A-Z) باشد."};

        public override IdentityError PasswordTooShort(int length) => new IdentityError {Code=nameof(PasswordTooShort),Description=$"کلمه عبور باید حداقل شامل {length} کاراکتر باشد."};

        public override IdentityError InvalidUserName(string userName) => new IdentityError {Code=nameof(InvalidUserName),Description="نام کاربری باید شامل کاراکترهای (0-9) و (a-z) باشد."};

        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = nameof(DuplicateEmail), Description = $"شما با ایمیل '{email}' قبلا ثبت نام کرده اید." };

        public override IdentityError DuplicateRoleName(string role) => new IdentityError { Code = nameof(DuplicateRoleName), Description = $"نقش '{role}' تکراری است." };
    }
}
