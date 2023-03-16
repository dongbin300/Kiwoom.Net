using Kiwoom.Net.Enums;

using System.Collections.Generic;

namespace Kiwoom.Net.Objects.Models
{
    public class StockItem
    {
        /// <summary>
        /// 종목코드
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 종목명
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 시세 데이터
        /// </summary>
        public List<Quote> Quotes { get; set; } = new List<Quote>();

        /// <summary>
        /// 거래소 구분
        /// </summary>
        public KiwoomMarket Market { get; set; } = KiwoomMarket.장내;
    }
}
