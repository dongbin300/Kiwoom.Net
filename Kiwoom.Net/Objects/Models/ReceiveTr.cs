using AxKHOpenAPILib;

namespace Kiwoom.Net.Objects.Models
{
    public class ReceiveTr
    {
        public AxKHOpenAPI Api { get; set; }
        public string Tr { get; set; }
        public string Req { get; set; }
        public StockItem Item { get; set; }

        public ReceiveTr(AxKHOpenAPI api, string tr, string req, StockItem item)
        {
            Api = api;
            Tr = tr;
            Req = req;
            Item = item;
        }
    }
}
