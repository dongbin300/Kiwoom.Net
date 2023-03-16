using System;
using System.Collections.Generic;

namespace Kiwoom.Net.Extensions
{
    public static class StringExtension
    {
        public static DateTime ToDate(this string data)
        {
            return DateTime.Parse($"{data.Substring(0, 4)}-{data.Substring(4, 2)}-{data.Substring(6, 2)}");
        }

        public static DateTime ToDateTime(this string data)
        {
            return DateTime.Parse($"{data.Substring(0, 4)}-{data.Substring(4, 2)}-{data.Substring(6, 2)} {data.Substring(8, 2)}:{data.Substring(10, 2)}:{data.Substring(12, 2)}");
        }

        public static IEnumerable<string> SplitSemicolon(this string data)
        {
            return data.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> SplitPipe(this string data)
        {
            return data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> SplitCaret(this string data)
        {
            return data.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
