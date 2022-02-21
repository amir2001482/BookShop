using MD.PersianDateTime.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Classes
{
    public class ConvertData : IConvertData
    {
        public DateTime ConvertShamsiToMiladi(string Date)
        {
            PersianDateTime persianDateTime = PersianDateTime.Parse(Date);
            return persianDateTime.ToDateTime();
        }

        public string ConvertMiladiToShamsi(DateTime Date, string Format)
        {
            PersianDateTime persianDateTime = new PersianDateTime(Date);
            return persianDateTime.ToString(Format);
        }
    }
}
