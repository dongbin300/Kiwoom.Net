using Kiwoom.Net.Extensions;
using Kiwoom.Net.Objects.Models;

using System;

namespace Kiwoom.Net.Clients
{
    public class KiwoomTrHandler
    {
        private static string GetCommDataToString(ReceiveTr receiveTr, string key)
        {
            return receiveTr.Api.GetCommData(receiveTr.Tr, receiveTr.Req, 0, key).Trim();
        }

        private static int GetCommDataToInt(ReceiveTr receiveTr, string key)
        {
            var result = GetCommDataToString(receiveTr, key);
            return string.IsNullOrWhiteSpace(result) ? 0 : int.Parse(result);
        }

        private static decimal GetCommDataToDecimal(ReceiveTr receiveTr, string key)
        {
            var result = GetCommDataToString(receiveTr, key);
            return string.IsNullOrWhiteSpace(result) ? 0 : decimal.Parse(result);
        }

        private static DateTime GetCommDataToDateTime(ReceiveTr receiveTr, string key)
        {
            var result = GetCommDataToString(receiveTr, key);
            return string.IsNullOrWhiteSpace(result) ? DateTime.MinValue : result.ToDateTime();
        }

        public static void OPT10001(ReceiveTr receiveTr)
        {
            receiveTr.Item.종목코드 = GetCommDataToString(receiveTr, "종목코드");
            receiveTr.Item.종목명 = GetCommDataToString(receiveTr, "종목명");
            receiveTr.Item.결산월 = GetCommDataToInt(receiveTr, "결산월");
            receiveTr.Item.액면가 = GetCommDataToDecimal(receiveTr, "액면가");
            receiveTr.Item.자본금 = GetCommDataToDecimal(receiveTr, "자본금");
            receiveTr.Item.상장주식 = GetCommDataToInt(receiveTr, "상장주식");
            receiveTr.Item.신용비율 = GetCommDataToDecimal(receiveTr, "신용비율");
            receiveTr.Item.연중최고 = GetCommDataToInt(receiveTr, "연중최고");
            receiveTr.Item.시가총액 = GetCommDataToDecimal(receiveTr, "시가총액");
            receiveTr.Item.시가총액비중 = GetCommDataToDecimal(receiveTr, "시가총액비중");
            receiveTr.Item.외인소진률 = GetCommDataToDecimal(receiveTr, "외인소진률");
            receiveTr.Item.대용가 = GetCommDataToDecimal(receiveTr, "대용가");
            receiveTr.Item.Per = GetCommDataToDecimal(receiveTr, "PER");
            receiveTr.Item.Eps = GetCommDataToDecimal(receiveTr, "EPS");
            receiveTr.Item.Roe = GetCommDataToDecimal(receiveTr, "ROE");
            receiveTr.Item.Pbr = GetCommDataToDecimal(receiveTr, "PBR");
            receiveTr.Item.Ev = GetCommDataToDecimal(receiveTr, "EV");
            receiveTr.Item.Bps = GetCommDataToDecimal(receiveTr, "BPS");
            receiveTr.Item.매출액 = GetCommDataToDecimal(receiveTr, "매출액");
            receiveTr.Item.영업이익 = GetCommDataToDecimal(receiveTr, "영업이익");
            receiveTr.Item.당기순이익 = GetCommDataToDecimal(receiveTr, "당기순이익");
            receiveTr.Item.최고250 = GetCommDataToInt(receiveTr, "최고250");
            receiveTr.Item.최저250 = GetCommDataToInt(receiveTr, "최저250");
            receiveTr.Item.시가 = GetCommDataToInt(receiveTr, "시가");
            receiveTr.Item.고가 = GetCommDataToInt(receiveTr, "고가");
            receiveTr.Item.저가 = GetCommDataToInt(receiveTr, "저가");
            receiveTr.Item.상한가 = GetCommDataToInt(receiveTr, "상한가");
            receiveTr.Item.하한가 = GetCommDataToInt(receiveTr, "하한가");
            receiveTr.Item.기준가 = GetCommDataToInt(receiveTr, "기준가");
            receiveTr.Item.예상체결가 = GetCommDataToInt(receiveTr, "예상체결가");
            receiveTr.Item.예상체결수량 = GetCommDataToInt(receiveTr, "예상체결수량");
            receiveTr.Item.최고가일250 = GetCommDataToDateTime(receiveTr, "최고가일250");
            receiveTr.Item.최고가대비율250 = GetCommDataToDecimal(receiveTr, "최고가대비율250");
            receiveTr.Item.최저가일250 = GetCommDataToDateTime(receiveTr, "최저가일250");
            receiveTr.Item.최저가대비율250 = GetCommDataToDecimal(receiveTr, "최저가대비율250");
            receiveTr.Item.현재가 = GetCommDataToInt(receiveTr, "현재가");
            receiveTr.Item.대비기호 = GetCommDataToInt(receiveTr, "대비기호");
            receiveTr.Item.전일대비 = GetCommDataToInt(receiveTr, "전일대비");
            receiveTr.Item.등락율 = GetCommDataToDecimal(receiveTr, "등락율");
            receiveTr.Item.거래량 = GetCommDataToInt(receiveTr, "거래량");
            receiveTr.Item.거래대비 = GetCommDataToDecimal(receiveTr, "거래대비");
            receiveTr.Item.액면가단위 = GetCommDataToString(receiveTr, "액면가단위");
            receiveTr.Item.유통주식 = GetCommDataToInt(receiveTr, "유통주식");
            receiveTr.Item.유통비율 = GetCommDataToDecimal(receiveTr, "유통비율");
        }

        public static void OPT10079(ReceiveTr receiveTr)
        {
            var result = (object[,])receiveTr.Api.GetCommDataEx(receiveTr.Tr, receiveTr.Req);
            for (int i = 0; i < result.GetLength(0); i++)
            {
                receiveTr.Item.Quotes.Add(new Quote
                {
                    Time = result[i, 2].ToString().ToDateTime(),
                    Open = int.Parse(result[i, 3].ToString()),
                    High = int.Parse(result[i, 4].ToString()),
                    Low = int.Parse(result[i, 5].ToString()),
                    Close = int.Parse(result[i, 0].ToString()),
                    Volume = int.Parse(result[i, 1].ToString())
                });
            }
        }

        public static void OPT10080(ReceiveTr receiveTr)
        {
            var result = (object[,])receiveTr.Api.GetCommDataEx(receiveTr.Tr, receiveTr.Req);
            for (int i = 0; i < result.GetLength(0); i++)
            {
                receiveTr.Item.Quotes.Add(new Quote
                {
                    Time = result[i, 2].ToString().ToDateTime(),
                    Open = int.Parse(result[i, 3].ToString().Substring(1)),
                    High = int.Parse(result[i, 4].ToString().Substring(1)),
                    Low = int.Parse(result[i, 5].ToString().Substring(1)),
                    Close = int.Parse(result[i, 0].ToString().Substring(1)),
                    Volume = int.Parse(result[i, 1].ToString())
                });
            }
        }

        public static void OPT10081(ReceiveTr receiveTr)
        {
            var result = (object[,])receiveTr.Api.GetCommDataEx(receiveTr.Tr, receiveTr.Req);
            for (int i = 0; i < result.GetLength(0); i++)
            {
                receiveTr.Item.Quotes.Add(new Quote
                {
                    Time = result[i, 4].ToString().ToDate(),
                    Open = int.Parse(result[i, 5].ToString()),
                    High = int.Parse(result[i, 6].ToString()),
                    Low = int.Parse(result[i, 7].ToString()),
                    Close = int.Parse(result[i, 1].ToString()),
                    Volume = int.Parse(result[i, 2].ToString())
                });
            }
        }

        public static void OPT10082(ReceiveTr receiveTr)
        {
            var result = (object[,])receiveTr.Api.GetCommDataEx(receiveTr.Tr, receiveTr.Req);
            for (int i = 0; i < result.GetLength(0); i++)
            {
                receiveTr.Item.Quotes.Add(new Quote
                {
                    Time = result[i, 3].ToString().ToDate(),
                    Open = int.Parse(result[i, 4].ToString()),
                    High = int.Parse(result[i, 5].ToString()),
                    Low = int.Parse(result[i, 6].ToString()),
                    Close = int.Parse(result[i, 0].ToString()),
                    Volume = int.Parse(result[i, 1].ToString())
                });
            }
        }

        public static void OPT10083(ReceiveTr receiveTr)
        {
            var result = (object[,])receiveTr.Api.GetCommDataEx(receiveTr.Tr, receiveTr.Req);
            for (int i = 0; i < result.GetLength(0); i++)
            {
                receiveTr.Item.Quotes.Add(new Quote
                {
                    Time = result[i, 3].ToString().ToDate(),
                    Open = int.Parse(result[i, 4].ToString()),
                    High = int.Parse(result[i, 5].ToString()),
                    Low = int.Parse(result[i, 6].ToString()),
                    Close = int.Parse(result[i, 0].ToString()),
                    Volume = int.Parse(result[i, 1].ToString())
                });
            }
        }

        public static void OPT10094(ReceiveTr receiveTr)
        {
            var result = (object[,])receiveTr.Api.GetCommDataEx(receiveTr.Tr, receiveTr.Req);
            for (int i = 0; i < result.GetLength(0); i++)
            {
                receiveTr.Item.Quotes.Add(new Quote
                {
                    Time = result[i, 3].ToString().ToDate(),
                    Open = int.Parse(result[i, 4].ToString()),
                    High = int.Parse(result[i, 5].ToString()),
                    Low = int.Parse(result[i, 6].ToString()),
                    Close = int.Parse(result[i, 0].ToString()),
                    Volume = int.Parse(result[i, 1].ToString())
                });
            }
        }

        public static void OPT10095(ReceiveTr receiveTr)
        {

        }
    }
}
