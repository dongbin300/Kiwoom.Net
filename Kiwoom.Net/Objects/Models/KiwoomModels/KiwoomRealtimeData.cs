namespace Kiwoom.Net.Objects.Models.KiwoomModels
{
    public class KiwoomRealtimeData
    {
        /// <summary>
        /// 종목
        /// </summary>
        public StockItem Item { get; set; }

        /// <summary>
        /// 리얼 타입
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 실시간 데이터 전문
        /// </summary>
        public string Data { get; set; }

        public KiwoomRealtimeData(StockItem item, string type, string data)
        {
            Item = item;
            Type = type;
            Data = data;
        }
    }
}
