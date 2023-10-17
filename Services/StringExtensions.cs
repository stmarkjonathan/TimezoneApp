using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimezoneApp.Services
{
    public static class StringExtensions
    {

        public static bool Contains(this string source, string strCheck, StringComparison cmp)
        {
            return source?.IndexOf(strCheck, cmp) >= 0;
        }
    }
}
