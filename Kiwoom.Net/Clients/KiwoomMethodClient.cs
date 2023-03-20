using AxKHOpenAPILib;

using Kiwoom.Net.Enums;
using Kiwoom.Net.Extensions;
using Kiwoom.Net.Objects.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Kiwoom.Net.Clients
{
    public class KiwoomMethodClient
    {
        /// <summary>
        /// 키움 Open API 컨트롤
        /// </summary>
        private AxKHOpenAPI api;

        /// <summary>
        /// 키움 데이터 저장소
        /// </summary>
        private KiwoomClientDataStorage data;

        public KiwoomMethodClient(AxKHOpenAPI api, KiwoomClientDataStorage data)
        {
            this.api = api;
            this.data = data;
        }

        /// <summary>
        /// 로그인 윈도우를 실행한다.
        /// 0 - 성공, 음수값은 실패
        /// 로그인이 성공하거나 실패하는 경우 OnEventConnect 이벤트가 발생하고 이벤트의 인자 값으로 로그인 성공 여부를 알 수 있다.
        /// </summary>
        /// <returns></returns>
        public int CommConnect()
        {
            return api.CommConnect();
        }

        /// <summary>
        /// Tran을 서버로 송신한다.
        /// PrevNext - 0:조회, 2:연속
        /// </summary>
        public string CommRqData(string requestId, string screenNumber, string trCode, int prevNext)
        {
            return api.CommRqData(requestId, trCode, prevNext, screenNumber).ToErrorString();
        }

        /// <summary>
        /// 로그인 정보를 반환한다.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetLoginInfo(string key)
        {
            return api.GetLoginInfo(key);
        }

        /// <summary>
        /// 주식 주문을 서버로 전송한다.
        /// 시장가, 최유리지정가, 최우선지정가, 시장가IOC, 최유리IOC, 시장가FOK, 최유리FOK, 장전시간외, 장후시간외 주문시 주문가격을 입력하지 않습니다.
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="screenNumber"></param>
        /// <param name="accountNumber"></param>
        /// <param name="orderType"></param>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="transactionType"></param>
        /// <param name="originalOrderNumber"></param>
        /// <returns></returns>
        public string SendOrder(string requestId, string screenNumber, string accountNumber, KiwoomOrderType orderType, StockItem item, int quantity, int price, KiwoomTransactionType transactionType, string originalOrderNumber = "")
        {
            return api.SendOrder(requestId, screenNumber, accountNumber, (int)orderType, item.종목코드, quantity, price, transactionType.ToCode(), originalOrderNumber).ToErrorString();
        }

        /// <summary>
        /// 신용주식 주문을 서버로 전송한다.
        /// 신용매수 주문
        /// - 신용구분값 “03”, 대출일은 “공백”
        /// 신용매도 융자상환 주문
        /// - 신용구분값 “33”, 대출일은 종목별 대출일 입력
        /// - OPW00005/OPW00004 TR조회로 대출일 조회
        /// 신용매도 융자합 주문시
        /// - 신용구분값 “99”, 대출일은 “99991231”
        /// - 단 신용잔고 5개까지만 융자합 주문가능
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="screenNumber"></param>
        /// <param name="accountNumber"></param>
        /// <param name="orderType"></param>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="transactionType"></param>
        /// <param name="creditType"></param>
        /// <param name="loanDate"></param>
        /// <param name="originalOrderNumber"></param>
        /// <returns></returns>
        public string SendOrderCredit(string requestId, string screenNumber, string accountNumber, KiwoomOrderType orderType, StockItem item, int quantity, int price, KiwoomTransactionType transactionType, KiwoomCreditType creditType, DateTime loanDate, string originalOrderNumber = "")
        {
            return api.SendOrderCredit(requestId, screenNumber, accountNumber, (int)orderType, item.종목코드, quantity, price, transactionType.ToCode(), creditType.ToCode(), loanDate.ToString("yyyyMMdd"), originalOrderNumber).ToErrorString();
        }

        /// <summary>
        /// Tran 입력 값을 서버통신 전에 입력한다.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void SetInputValue(string id, string value)
        {
            api.SetInputValue(id, value);
        }

        /// <summary>
        /// 화면 내 모든 리얼데이터 요청을 제거한다.
        /// 화면을 종료할 때 반드시 위 함수를 호출해야 한다.
        /// </summary>
        /// <param name="screenNumber"></param>
        public void DisconnectRealData(string screenNumber)
        {
            api.DisconnectRealData(screenNumber);
        }

        /// <summary>
        /// 레코드 반복횟수를 반환한다.
        /// </summary>
        /// <param name="trCode"></param>
        /// <param name="recordName"></param>
        /// <returns></returns>
        public int GetRepeatCnt(string trCode, string recordName)
        {
            return api.GetRepeatCnt(trCode, recordName);
        }

        /// <summary>
        /// 복수종목조회 Tran을 서버로 송신한다.
        /// SearchType – 0:주식관심종목정보, 3:선물옵션관심종목정보
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="screenNumber"></param>
        /// <param name="items"></param>
        /// <param name="searchType"></param>
        /// <param name="isNext"></param>
        /// <returns></returns>
        public string CommKwRqData(string requestId, string screenNumber, IEnumerable<StockItem> items, int searchType, bool isNext)
        {
            return api.CommKwRqData(string.Join(";", items), isNext ? 1 : 0, items.Count(), searchType, requestId, screenNumber).ToErrorString();
        }

        /// <summary>
        /// OpenAPI모듈의 경로를 반환한다.
        /// </summary>
        /// <returns></returns>
        public string GetApiModulePath()
        {
            return api.GetAPIModulePath();
        }

        /// <summary>
        /// 시장구분에 따른 종목코드를 반환한다.
        /// </summary>
        /// <param name="market"></param>
        /// <returns></returns>
        public IEnumerable<string> GetCodeListByMarket(KiwoomMarket market)
        {
            return api.GetCodeListByMarket(((int)market).ToString()).SplitSemicolon();
        }

        /// <summary>
        /// 현재접속상태를 반환한다.
        /// </summary>
        /// <returns></returns>
        public bool GetConnectState()
        {
            return api.GetConnectState() == 1;
        }

        /// <summary>
        /// 종목코드의 한글명을 반환한다.
        /// 장내외, 지수선옵, 주식선옵 검색 가능.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetMasterCodeName(string itemCode)
        {
            return api.GetMasterCodeName(itemCode);
        }

        /// <summary>
        /// 종목코드의 상장주식수를 반환한다.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int GetMasterListedStockCnt(string itemCode)
        {
            return api.GetMasterListedStockCnt(itemCode);
        }

        /// <summary>
        /// 종목코드의 감리구분을 반환한다.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns>정상, 투자주의, 투자경고, 투자위험, 투자주의환기종목</returns>
        public string GetMasterConstruction(string itemCode)
        {
            return api.GetMasterConstruction(itemCode);
        }

        /// <summary>
        /// 종목코드의 상장일을 반환한다.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public DateTime GetMasterListedStockDate(string itemCode)
        {
            return DateTime.Parse(api.GetMasterListedStockDate(itemCode));
        }

        /// <summary>
        /// 종목코드의 전일가를 반환한다.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int GetMasterLastPrice(string itemCode)
        {
            return int.Parse(api.GetMasterLastPrice(itemCode));
        }

        /// <summary>
        /// 종목코드의 종목상태를 반환한다.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns>정상, 증거금100%, 거래정지, 관리종목, 감리종목, 투자유의종목, 담보대출, 액면분할, 신용가능</returns>
        public string GetMasterStockState(string itemCode)
        {
            return api.GetMasterStockState(itemCode);
        }

        /// <summary>
        /// 레코드의 반복개수를 반환한다.
        /// </summary>
        /// <param name="recordName"></param>
        /// <returns></returns>
        public int GetDataCount(string recordName)
        {
            return api.GetDataCount(recordName);
        }

        /// <summary>
        /// 레코드의 반복순서와 아이템의 출력순서에 따라 수신데이터를 반환한다.
        /// </summary>
        /// <param name="recordName"></param>
        /// <param name="repeatIndex"></param>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        public string GetOutputValue(string recordName, int repeatIndex, int itemIndex)
        {
            return api.GetOutputValue(recordName, repeatIndex, itemIndex);
        }

        /// <summary>
        /// 수신 데이터를 반환한다.
        /// </summary>
        /// <param name="trCode"></param>
        /// <param name="recordName"></param>
        /// <param name="index"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string GetCommData(string trCode, string recordName, int index, string itemName)
        {
            return api.GetCommData(trCode, recordName, index, itemName);
        }

        /// <summary>
        /// 실시간 시세 데이터를 반환한다.
        /// Id - 실시간 아이템
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCommRealData(StockItem item, KiwoomFID id)
        {
            return api.GetCommRealData(item.종목코드, (int)id);
        }

        /// <summary>
        /// 체결잔고 데이터를 반환한다.
        /// Id - 체결잔고 아이템
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetChejanData(KiwoomFID id)
        {
            return api.GetChejanData((int)id);
        }

        /// <summary>
        /// 테마코드와 테마명을 반환한다.
        /// SortType – 정렬순서 (0:코드순, 1:테마순)
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public IEnumerable<ThemeItem> GetThemeGroupList(int sortType)
        {
            var result = new List<ThemeItem>();
            var themeGroupList = api.GetThemeGroupList(sortType);
            var themeStrings = themeGroupList.SplitSemicolon();

            foreach(var themeString in themeStrings)
            {
                var segments = themeString.SplitPipe();
                result.Add(new ThemeItem
                {
                    Code = segments.ElementAt(0),
                    Name = segments.ElementAt(1)
                });
            }

            return result;
        }

        /// <summary>
        /// 테마코드에 소속된 종목코드를 반환한다.
        /// </summary>
        /// <param name="themeCode"></param>
        /// <returns></returns>
        public IEnumerable<string> GetThemeGroupCode(string themeCode)
        {
            return api.GetThemeGroupCode(themeCode).SplitSemicolon();
        }

        /// <summary>
        /// 지수선물 리스트를 반환한다.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetFutureList()
        {
            return api.GetFutureList().SplitSemicolon();
        }

        /// <summary>
        /// 지수선물 코드를 반환한다.
        /// Index – 0~3 지수선물코드, 4~7 지수스프레드
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetFutureCodeByIndex(int index)
        {
            return api.GetFutureCodeByIndex(index);
        }

        /// <summary>
        /// 지수옵션 행사가 리스트를 반환한다.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<decimal> GetActPriceList()
        {
            return api.GetActPriceList().SplitSemicolon().Select(x=>decimal.Parse(x));
        }

        /// <summary>
        /// 지수옵션 월물 리스트를 반환한다
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetMonthList()
        {
            return api.GetMonthList().SplitSemicolon();
        }

        /// <summary>
        /// 행사가와 월물 콜풋으로 종목코드를 구한다.
        /// ActPrice – 행사가(소수점포함)
        /// CallPut – 콜풋구분 2:콜, 3:풋
        /// Month – 월물(6자리)
        /// </summary>
        /// <param name="actPrice"></param>
        /// <param name="callPut"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetOptionCode(string actPrice, int callPut, string month)
        {
            return api.GetOptionCode(actPrice, callPut, month);
        }

        /// <summary>
        /// 입력된 종목코드와 동일한 행사가의 코드중 입력한 월물의 코드를 구한다.
        /// ItemCode – 종목코드
        /// CallPut – 콜풋구분 2:콜, 3:풋
        /// Month – 월물(6자리)
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="callPut"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetOptionCodeByMonth(string itemCode, int callPut, string month)
        {
            return api.GetOptionCodeByMonth(itemCode, callPut, month);
        }

        /// <summary>
        /// 입력된 종목코드와 동일한 월물의 코드중 입력한 틱만큼 벌어진 코드를 구한다.
        /// ItemCode – 종목코드
        /// CallPut – 콜풋구분 2:콜, 3:풋
        /// Tick – 행사가 틱
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="callPut"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public string GetOptionCodeByActPrice(string itemCode, int callPut, int tick)
        {
            return api.GetOptionCodeByActPrice(itemCode, callPut, tick);
        }

        /// <summary>
        /// 주식선물 코드 리스트를 반환한다.
        /// </summary>
        /// <param name="baseAssetCode"></param>
        /// <returns></returns>
        public IEnumerable<string> GetSFutureList(string baseAssetCode)
        {
            return api.GetSFutureList(baseAssetCode).SplitSemicolon();
        }

        /// <summary>
        /// 주식선물 코드를 반환한다.
        /// Index – 0~3 지수선물코드, 4~7 지수스프레드, 8~11 스타 선물, 12~ 스타 스프레드
        /// </summary>
        /// <param name="baseAssetCode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetSFutureCodeByIndex(string baseAssetCode, int index)
        {
            return api.GetSFutureCodeByIndex(baseAssetCode, index);
        }

        /// <summary>
        /// 주식옵션 행사가 리스트를 반환한다.
        /// </summary>
        /// <param name="baseAssetType"></param>
        /// <returns></returns>
        public IEnumerable<string> GetSActPriceList(string baseAssetType)
        {
            return api.GetSActPriceList(baseAssetType).SplitSemicolon();
        }

        /// <summary>
        /// 주식옵션 월물 리스트를 반환한다.
        /// </summary>
        /// <param name="baseAssetType"></param>
        /// <returns></returns>
        public IEnumerable<string> GetSMonthList(string baseAssetType)
        {
            return api.GetSMonthList(baseAssetType).SplitSemicolon();
        }

        /// <summary>
        /// 주식옵션 코드를 반환한다.
        /// </summary>
        /// <param name="baseAssetType"></param>
        /// <param name="actPrice"></param>
        /// <param name="callPut"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetSOptionCode(string baseAssetType, string actPrice, int callPut, string month)
        {
            return api.GetSOptionCode(baseAssetType, actPrice, callPut, month);
        }

        /// <summary>
        /// 입력한 주식옵션 코드에서 월물만 변경하여 반환한다.
        /// </summary>
        /// <param name="baseAssetType"></param>
        /// <param name="itemCode"></param>
        /// <param name="callPut"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetSOptionCodeByMonth(string baseAssetType, string itemCode, int callPut, string month)
        {
            return api.GetSOptionCodeByMonth(baseAssetType, itemCode, callPut, month);
        }

        /// <summary>
        /// 입력한 주식옵션 코드에서 행사가만 변경하여 반환한다.
        /// </summary>
        /// <param name="baseAssetType"></param>
        /// <param name="itemCode"></param>
        /// <param name="callPut"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public string GetSOptionCodeByActPrice(string baseAssetType, string itemCode, int callPut, int tick)
        {
            return api.GetSOptionCodeByActPrice(baseAssetType, itemCode, callPut, tick);
        }

        /// <summary>
        /// 주식선옵 기초자산코드/종목명을 반환한다.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SfoBasisAsset> GetSFOBasisAssetList()
        {
            var result = new List<SfoBasisAsset>();
            var sfoBasisAssetList = api.GetSFOBasisAssetList();
            var sfoBasisAssetStrings = sfoBasisAssetList.SplitSemicolon();

            foreach (var sfoBasisAssetString in sfoBasisAssetStrings)
            {
                var segments = sfoBasisAssetString.SplitPipe();
                result.Add(new SfoBasisAsset
                {
                    Code = segments.ElementAt(0),
                    Name = segments.ElementAt(1)
                });
            }

            return result;
        }

        /// <summary>
        /// 지수옵션 ATM을 반환한다.
        /// </summary>
        /// <returns></returns>
        public string GetOptionAtm()
        {
            return api.GetOptionATM();
        }

        /// <summary>
        /// 주식옵션 ATM을 반환한다.
        /// </summary>
        /// <returns></returns>
        public string GetSOptionAtm(string baseAssetType)
        {
            return api.GetSOptionATM(baseAssetType);
        }

        /// <summary>
        /// 회원사 코드와 이름을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BranchItem> GetBranchCodeName()
        {
            var result = new List<BranchItem>();
            var branchList = api.GetBranchCodeName();
            var branchStrings = branchList.SplitSemicolon();

            foreach (var branchString in branchStrings)
            {
                var segments = branchString.SplitPipe();
                result.Add(new BranchItem
                {
                    Code = segments.ElementAt(0),
                    Name = segments.ElementAt(1)
                });
            }

            return result;
        }

        /// <summary>
        /// 다수의 아이디로 자동로그인이 필요할 때 사용한다.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string SetInfoData(string data)
        {
            return api.SetInfoData(data).ToErrorString();
        }

        /// <summary>
        /// 실시간 등록을 한다.
        /// CodeList – 실시간 등록할 종목코드
        /// FidList – 실시간 등록할 FID
        /// RealType – "0", "1" 타입
        /// 
        /// RealType이 "0" 으로 하면 같은화면에서 다른종목 코드로 실시간 등록을 하게 되면 마지막에 사용한 종목코드만 실시간 등록이 되고 기존에 있던 종목은 실시간이 자동 해지됨. "1"로 하면 같은화면에서 다른 종목들을 추가하게 되면 기존에 등록한 종목도 함께 실시간 시세를 받을 수 있음. 꼭 같은 화면이여야 하고 최초 실시간 등록은 "0"으로 하고 이후부터 "1"로 등록해야함
        /// </summary>
        /// <param name="screenNumber"></param>
        /// <param name="itemCodes"></param>
        /// <param name="fids"></param>
        /// <param name="realType"></param>
        /// <returns></returns>
        public string SetRealReg(string screenNumber, IEnumerable<StockItem> itemCodes, IEnumerable<KiwoomFID> fids, string realType)
        {
            return api.SetRealReg(screenNumber, string.Join(";", itemCodes), string.Join(";", fids.Select(x=>(int)x)), realType).ToErrorString();
        }

        /// <summary>
        /// 종목별 실시간 해제.
        /// SetRealReg() 함수로 실시간 등록한 종목만 실시간 해제 할 수 있다.
        /// </summary>
        /// <param name="screenNumber"></param>
        /// <param name="deleteCode"></param>
        public void SetRealRemove(string screenNumber, string deleteCode)
        {
            api.SetRealRemove(screenNumber, deleteCode);
        }

        /// <summary>
        /// 서버에 저장된 사용자 조건식을 조회해서 임시로 파일에 저장.
        /// System 폴더에 아이디_NewSaveIndex.dat파일로 저장된다. Ocx가 종료되면 삭제시킨다. 조건검색 사용시 이함수를 최소 한번은 호출해야 조건검색을 할 수 있다. 영웅문에서 사용자 조건을 수정 및 추가하였을 경우에도 최신의 사용자 조건을 받고 싶으면 다시 조회해야한다.
        /// </summary>
        public void GetConditionLoad()
        {
            api.GetConditionLoad();
        }

        /// <summary>
        /// 조건검색 조건명 리스트를 받아온다.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConditionItem> GetConditionNameList()
        {
            var result = new List<ConditionItem>();
            var conditions = api.GetConditionNameList();
            var conditionStrings = conditions.SplitSemicolon();

            foreach (var conditionString in conditionStrings)
            {
                var segments = conditionString.SplitCaret();
                result.Add(new ConditionItem
                {
                    Code = segments.ElementAt(0),
                    Name = segments.ElementAt(1)
                });
            }

            return result;
        }

        /// <summary>
        /// 조건검색 종목조회TR송신한다.
        /// SearchType - 조회구분(0:일반조회, 1:실시간조회, 2:연속조회)
        /// 
        /// 단순 조건식에 맞는 종목을 조회하기 위해서는 조회구분을 0으로 하고, 실시간 조건검색을 하기 위해서는 조회구분을 1로 한다. OnReceiveTrCondition으로 결과값이 온다. 연속조회가 필요한 경우에는 응답받는 곳에서 연속조회 여부에 따라 연속조회를 송신하면된다.
        /// </summary>
        /// <param name="screenNumber"></param>
        /// <param name="conditionName"></param>
        /// <param name="index"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public bool SendCondition(string screenNumber, string conditionName, int index, int searchType)
        {
            return api.SendCondition(screenNumber, conditionName, index, searchType) == 1;
        }

        /// <summary>
        /// 조건검색 실시간 중지TR을 송신한다.
        /// 
        /// 해당 조건명의 실시간 조건검색을 중지하거나, 다른 조건명으로 바꿀 때 이전 조건명으로 실시간 조건검색을 반드시 중지해야한다. 화면 종료시에도 실시간 조건검색을 한 조건명으로 전부 중지해줘야 한다.
        /// </summary>
        /// <param name="screenNumber"></param>
        /// <param name="conditionName"></param>
        /// <param name="index"></param>
        public void SendConditionStop(string screenNumber, string conditionName, int index)
        {
            api.SendConditionStop(screenNumber, conditionName, index);
        }

        /// <summary>
        /// 차트 조회 데이터를 배열로 받아온다.
        /// 
        /// 조회 데이터가 많은 차트 경우 GetCommData()로 항목당 하나씩 받아오는 것 보다 한번에 데이터 전부를 받아서 사용자가 처리할 수 있도록 배열로 받는다.
        /// </summary>
        /// <param name="trCode"></param>
        /// <param name="recordName"></param>
        public void GetCommDataEx(string trCode, string recordName)
        {
            api.GetCommDataEx(trCode, recordName);
        }
    }
}
