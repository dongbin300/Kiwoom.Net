using AxKHOpenAPILib;

using Kiwoom.Net.Extensions;
using Kiwoom.Net.Objects.Models;
using Kiwoom.Net.Objects.Models.KiwoomModels;

using System;
using System.Reflection;

namespace Kiwoom.Net.Clients
{
    public class KiwoomEventClient
    {
        /// <summary>
        /// 키움 Open API 컨트롤
        /// </summary>
        private AxKHOpenAPI api;

        /// <summary>
        /// 키움 데이터 저장소
        /// </summary>
        private KiwoomClientDataStorage data;

        public KiwoomEventClient(AxKHOpenAPI api, KiwoomClientDataStorage data)
        {
            this.api = api;
            this.data = data;
            this.api.OnEventConnect += OnEventConnect;
            this.api.OnReceiveChejanData += OnReceiveChejanData;
            this.api.OnReceiveConditionVer += OnReceiveConditionVer;
            this.api.OnReceiveInvestRealData += OnReceiveInvestRealData;
            this.api.OnReceiveMsg += OnReceiveMsg;
            this.api.OnReceiveRealCondition += OnReceiveRealCondition;
            this.api.OnReceiveRealData += OnReceiveRealData;
            this.api.OnReceiveTrCondition += OnReceiveTrCondition;
            this.api.OnReceiveTrData += OnReceiveTrData;
        }

        /// <summary>
        /// 서버통신 후 데이터를 받은 시점을 알려준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveTrData(object sender, _DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            var tr = e.sTrCode;
            var req = e.sRQName;
            var item = data.GetItemByCode(api.GetCommData(tr, req, 0, "종목코드").Trim());

            var receiveTr = new ReceiveTr(api, tr, req, item);

            Type.GetType("Kiwoom.Net.Clients.KiwoomTrHandler").GetMethod(req, BindingFlags.Static | BindingFlags.Public).Invoke(null, new object[] { receiveTr });

            e.sRQName = e.sTrCode = "";
        }

        /// <summary>
        /// 조건검색 조회응답으로 종목리스트를 구분자(";")로 붙어서 받는 시점.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveTrCondition(object sender, _DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {

        }

        /// <summary>
        /// 실시간데이터를 받은 시점을 알려준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveRealData(object sender, _DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            var realtimeData = new KiwoomRealtimeData(data.GetItemByCode(e.sRealKey), e.sRealType, e.sRealData);
            data.RealtimeData.Add(realtimeData);
        }

        /// <summary>
        /// 조건검색 실시간 편입,이탈 종목을 받을 시점을 알려준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveRealCondition(object sender, _DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {

        }

        /// <summary>
        /// 서버통신 후 메시지를 받은 시점을 알려준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveMsg(object sender, _DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {

        }

        /// <summary>
        /// 실시간데이터를 받은 시점을 알려준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveInvestRealData(object sender, _DKHOpenAPIEvents_OnReceiveInvestRealDataEvent e)
        {

        }

        /// <summary>
        /// 로컬에 사용자 조건식 저장 성공 여부를 확인하는 시점
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveConditionVer(object sender, _DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {

        }

        /// <summary>
        /// 체결데이터를 받은 시점을 알려준다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReceiveChejanData(object sender, _DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {

        }

        /// <summary>
        /// 로그인 성공 시 이벤트
        /// 실패 시에는 컨트롤 내부에서 메시지박스를 띄워서 다음으로 진행이 안 됨.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEventConnect(object sender, _DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode != 0)
            {
                throw new Exception(e.nErrCode.ToErrorString());
            }
        }
    }
}
