namespace Kiwoom.Net.Extensions
{
    public static class IntExtension
    {
        /// <summary>
        /// 에러코드를 문자열로 변환
        /// 에러가 없으면 빈 문자열로 반환
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string ToErrorString(this int errorCode)
        {
            switch (errorCode)
            {
                case -10: return "실패";
                case -11: return "조건번호 없음";
                case -12: return "조건번호와 조건식 틀림";
                case -13: return "조건검색 조회요청 초과";
                case -100: return "사용자정보 교환실패";
                case -101: return "서버접속 실패";
                case -102: return "버전처리 실패";
                case -103: return "개인방화벽 실패";
                case -104: return "메모리보호 실패";
                case -105: return "함수입력값 오류";
                case -106: return "통신 연결종료";
                case -200: return "시세조회 과부하";
                case -201: return "전문작성 초기화 실패";
                case -202: return "전문작성 입력값 오류";
                case -203: return "데이터 없음";
                case -204: return "조회 가능한 종목수 초과";
                case -205: return "데이터수신 실패";
                case -206: return "조회 가능한 FID수초과";
                case -207: return "실시간 해제 오류";
                case -300: return "입력값 오류";
                case -301: return "계좌 비밀번호 없음";
                case -302: return "타인계좌사용 오류";
                case -303: return "주문가격이 20억원을 초과";
                case -304: return "주문가격이 50억원을 초과";
                case -305: return "주문수량이 총발행주수의 1%초과오류";
                case -306: return "주문수량이 총발행주수의 3%초과오류";
                case -307: return "주문전송 실패";
                case -308:
                case -311: return "주문전송 과부하";
                case -309: return "주문수량 300계약 초과";
                case -310: return "주문수량 500계약 초과";
                case -340: return "계좌정보없음";
                case -500: return "종목코드없음";
                default: return string.Empty;
            }
        }
    }
}
