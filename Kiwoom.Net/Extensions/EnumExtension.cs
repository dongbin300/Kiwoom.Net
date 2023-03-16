using Kiwoom.Net.Enums;

namespace Kiwoom.Net.Extensions
{
    public static class EnumExtension
    {
        public static string ToCode(this KiwoomTransactionType type)
        {
            switch (type)
            {
                case KiwoomTransactionType.지정가: return "00";
                case KiwoomTransactionType.시장가: return "03";
                case KiwoomTransactionType.조건부지정가: return "05";
                case KiwoomTransactionType.최유리지정가: return "06";
                case KiwoomTransactionType.최우선지정가: return "07";
                case KiwoomTransactionType.지정가IOC: return "10";
                case KiwoomTransactionType.시장가IOC: return "13";
                case KiwoomTransactionType.최유리IOC: return "16";
                case KiwoomTransactionType.지정가FOK: return "20";
                case KiwoomTransactionType.시장가FOK: return "23";
                case KiwoomTransactionType.최유리FOK: return "26";
                case KiwoomTransactionType.장전시간외종가: return "61";
                case KiwoomTransactionType.시간외단일가: return "62";
                case KiwoomTransactionType.장후시간외종가: return "81";
                default: return "";
            }
        }

        public static string ToCode(this KiwoomCreditType type)
        {
            switch (type)
            {
                case KiwoomCreditType.신용매수: return "03";
                case KiwoomCreditType.신용매도_융자상환: return "33";
                case KiwoomCreditType.신용매도_융자합: return "99";
                default: return "";
            }
        }
    }
}
