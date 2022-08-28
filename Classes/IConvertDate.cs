using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Classes
{
    public interface IConvertDate
    {
        DateTime ConvertShamsiToMiladi(string Date);
        string ConvertMiladiToShamsi(DateTime Date, string Format);
    }
}
