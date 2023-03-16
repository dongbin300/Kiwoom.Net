using Kiwoom.Net.Objects.Models;
using Kiwoom.Net.Objects.Models.KiwoomModels;

using System.Collections.Generic;

namespace Kiwoom.Net.Clients
{
    public class KiwoomClientDataStorage
    {
        /// <summary>
        /// 주식 종목 리스트
        /// </summary>
        public List<StockItem> Items { get; set; } = new List<StockItem>();

        /// <summary>
        /// 실시간 데이터
        /// </summary>
        public List<KiwoomRealtimeData> RealtimeData { get; set; } = new List<KiwoomRealtimeData>();

        public StockItem GetItemByCode(string code)
        {
            return Items.Find(x => x.Code.Equals(code));
        }

        public StockItem GetItemByName(string name)
        {
            return Items.Find(x => x.Name.Equals(name));
        }
    }
}
