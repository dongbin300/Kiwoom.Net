using System;

namespace Kiwoom.Net.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToKiwoomDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
