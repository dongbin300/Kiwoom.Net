using Kiwoom.Net.Enums;
using Kiwoom.Net.Extensions;
using Kiwoom.Net.Objects.Models;
using Kiwoom.Net.Objects.Models.KiwoomModels;

using System;
using System.Text.RegularExpressions;

namespace Kiwoom.Net.Clients
{
    public class KiwoomClient
    {
        /// <summary>
        /// 화면 번호
        /// </summary>
        private readonly string screenNumber = "5000";

        /// <summary>
        /// 키움 메서드 클라이언트
        /// </summary>
        public KiwoomMethodClient Api;

        /// <summary>
        /// 키움 이벤트 클라이언트
        /// </summary>
        public KiwoomEventClient Event;

        /// <summary>
        /// 키움 데이터 저장소
        /// </summary>
        public KiwoomClientDataStorage Data;

        /// <summary>
        /// 객체 및 이벤트 등록
        /// </summary>
        /// <param name="api"></param>
        public KiwoomClient(AxKHOpenAPILib.AxKHOpenAPI api)
        {
            Data = new KiwoomClientDataStorage();
            Api = new KiwoomMethodClient(api, Data);
            Event = new KiwoomEventClient(api, Data);
        }

        /// <summary>
        /// 종목 코드 얻기
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private string GetItemCode(string itemId)
        {
            return Regex.IsMatch(itemId, @"[0-9]{6}") ? itemId : Data.Items.Find(x => x.종목명.Equals(itemId)).종목코드;
        }

        /// <summary>
        /// 로그인
        /// </summary>
        public void Login()
        {
            Api.CommConnect();
        }

        /// <summary>
        /// 모든 종목 불러오기
        /// </summary>
        public void LoadAllStockItems()
        {
            Data.Items.Clear();
            LoadStockItems(KiwoomMarket.장내);
            LoadStockItems(KiwoomMarket.코스닥);
            LoadStockItems(KiwoomMarket.코넥스);
            LoadStockItems(KiwoomMarket.ELW);
            LoadStockItems(KiwoomMarket.ETF);
            LoadStockItems(KiwoomMarket.리츠);
            LoadStockItems(KiwoomMarket.뮤추얼펀드);
            LoadStockItems(KiwoomMarket.신주인수권);
            LoadStockItems(KiwoomMarket.하이일드펀드);
            LoadStockItems(KiwoomMarket.K_OTC);
        }

        /// <summary>
        /// 거래소 종목 불러오기
        /// </summary>
        /// <param name="market"></param>
        public void LoadStockItems(KiwoomMarket market)
        {
            var codes = Api.GetCodeListByMarket(market);
            foreach (var code in codes)
            {
                string name = Api.GetMasterCodeName(code);
                Data.Items.Add(new StockItem
                {
                    종목코드 = code,
                    종목명 = name,
                    Market = market
                });
            }
        }

        /// <summary>
        /// 로그인한 사용자 정보를 반환한다.
        /// </summary>
        /// <returns></returns>
        public KiwoomUser GetLoginInfo()
        {
            return new KiwoomUser
            {
                AccountCount = int.Parse(Api.GetLoginInfo("ACCOUNT_CNT")),
                AccountNumbers = Api.GetLoginInfo("ACCNO").SplitSemicolon(),
                Id = Api.GetLoginInfo("USER_ID"),
                Name = Api.GetLoginInfo("USER_NAME"),
                IsKeyboardSecurity = Api.GetLoginInfo("KEY_BSECGB") == "0",
                IsFirewall = Api.GetLoginInfo("FIREW_SECGB") == "1",
            };
        }

        /// <summary>
        /// OPT10001 - 주식기본정보
        /// </summary>
        /// <param name="itemId"></param>
        public void 주식기본정보(string itemId)
        {
            var code = GetItemCode(itemId);
            Api.SetInputValue("종목코드", code);
            Api.CommRqData("OPT10001", screenNumber, "OPT10001", 0);
        }

        /// <summary>
        /// OPT10079 - 주식틱차트조회
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="interval"></param>
        /// <param name="수정주가구분"></param>
        public void 주식틱차트조회(string itemId, KiwoomTickInterval interval, bool 수정주가구분)
        {
            var code = GetItemCode(itemId);
            Api.SetInputValue("종목코드", code);
            Api.SetInputValue("틱범위", ((int)interval).ToString());
            Api.SetInputValue("수정주가구분", 수정주가구분 ? "1" : "0");
            Api.CommRqData("OPT10079", screenNumber, "OPT10079", 0);
        }

        /// <summary>
        /// OPT10080 - 주식분봉차트조회
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="interval"></param>
        /// <param name="수정주가구분"></param>
        public void 주식분봉차트조회(string itemId, KiwoomMinuteInterval interval, bool 수정주가구분)
        {
            var code = GetItemCode(itemId);
            Api.SetInputValue("종목코드", code);
            Api.SetInputValue("틱범위", ((int)interval).ToString());
            Api.SetInputValue("수정주가구분", 수정주가구분 ? "1" : "0");
            Api.CommRqData("OPT10080", screenNumber, "OPT10080", 0);
        }

        /// <summary>
        /// OPT10081 - 주식일봉차트조회
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="date"></param>
        /// <param name="수정주가구분"></param>
        public void 주식일봉차트조회(string itemId, DateTime date, bool 수정주가구분)
        {
            var code = GetItemCode(itemId);
            Api.SetInputValue("종목코드", code);
            Api.SetInputValue("기준일자", date.ToKiwoomDate());
            Api.SetInputValue("수정주가구분", 수정주가구분 ? "1" : "0");
            Api.CommRqData("OPT10081", screenNumber, "OPT10081", 0);
        }

        /// <summary>
        /// OPT10082 - 주식주봉차트조회
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="date"></param>
        /// <param name="endDate"></param>
        /// <param name="수정주가구분"></param>
        public void 주식주봉차트조회(string itemId, DateTime date, DateTime endDate, bool 수정주가구분)
        {
            var code = GetItemCode(itemId);
            Api.SetInputValue("종목코드", code);
            Api.SetInputValue("기준일자", date.ToKiwoomDate());
            Api.SetInputValue("끝일자", endDate.ToKiwoomDate());
            Api.SetInputValue("수정주가구분", 수정주가구분 ? "1" : "0");
            Api.CommRqData("OPT10082", screenNumber, "OPT10082", 0);
        }

        /// <summary>
        /// OPT10083 - 주식월봉차트조회
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="date"></param>
        /// <param name="endDate"></param>
        /// <param name="수정주가구분"></param>
        public void 주식월봉차트조회(string itemId, DateTime date, DateTime endDate, bool 수정주가구분)
        {
            var code = GetItemCode(itemId);
            Api.SetInputValue("종목코드", code);
            Api.SetInputValue("기준일자", date.ToKiwoomDate());
            Api.SetInputValue("끝일자", endDate.ToKiwoomDate());
            Api.SetInputValue("수정주가구분", 수정주가구분 ? "1" : "0");
            Api.CommRqData("OPT10083", screenNumber, "OPT10083", 0);
        }

        /// <summary>
        /// OPT10094 - 주식년봉차트조회
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="date"></param>
        /// <param name="endDate"></param>
        /// <param name="수정주가구분"></param>
        public void 주식년봉차트조회(string itemId, DateTime date, DateTime endDate, bool 수정주가구분)
        {
            var code = GetItemCode(itemId);
            Api.SetInputValue("종목코드", code);
            Api.SetInputValue("기준일자", date.ToKiwoomDate());
            Api.SetInputValue("끝일자", endDate.ToKiwoomDate());
            Api.SetInputValue("수정주가구분", 수정주가구분 ? "1" : "0");
            Api.CommRqData("OPT10094", screenNumber, "OPT10094", 0);
        }
    }
}
