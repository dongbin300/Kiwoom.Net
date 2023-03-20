using Kiwoom.Net.Enums;

using System;
using System.Collections.Generic;

namespace Kiwoom.Net.Objects.Models
{
    public class StockItem
    {
        public string 종목코드 { get; set; } = string.Empty;

        public string 종목명 { get; set; } = string.Empty;

        /// <summary>
        /// 시세 데이터
        /// </summary>
        public List<Quote> Quotes { get; set; } = new List<Quote>();

        /// <summary>
        /// 거래소 구분
        /// </summary>
        public KiwoomMarket Market { get; set; } = KiwoomMarket.장내;

        public int 결산월 { get; set; }
        public decimal 액면가 { get; set; }
        public decimal 자본금 { get; set; }
        public int 상장주식 { get; set; }
        public decimal 신용비율 { get; set; }
        public int 연중최고 { get; set; }
        public int 연중최저 { get; set; }
        public decimal 시가총액 { get; set; }
        public decimal 시가총액비중 { get; set; }
        public decimal 외인소진률 { get; set; }
        public decimal 대용가 { get; set; }
        public decimal Per { get; set; }
        public decimal Eps { get; set; }
        public decimal Roe { get; set; }
        public decimal Pbr { get; set; }
        public decimal Ev { get; set; }
        public decimal Bps { get; set; }
        public decimal 매출액 { get; set; }
        public decimal 영업이익 { get; set; }
        public decimal 당기순이익 { get; set; }
        public int 최고250 { get; set; }
        public int 최저250 { get; set; }
        public int 시가 { get; set; }
        public int 고가 { get; set; }
        public int 저가 { get; set; }
        public int 상한가 { get; set; }
        public int 하한가 { get; set; }
        public int 기준가 { get; set; }
        public int 예상체결가 { get; set; }
        public int 예상체결수량 { get; set; }
        public DateTime 최고가일250 { get; set; }
        public decimal 최고가대비율250 { get; set; }
        public DateTime 최저가일250 { get; set; }
        public decimal 최저가대비율250 { get; set; }
        public int 현재가 { get; set; }
        public int 대비기호 { get; set; }
        public int 전일대비 { get; set; }
        public decimal 등락율 { get; set; }
        public int 거래량 { get; set; }
        public decimal 거래대비 { get; set; }
        public string 액면가단위 { get; set; }
        public int 유통주식 { get; set; }
        public decimal 유통비율 { get; set; }
    }
}
