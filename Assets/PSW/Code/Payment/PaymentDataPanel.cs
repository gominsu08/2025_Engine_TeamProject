using GMS.Code.Core;
using PSW.Code.Container;
using PSW.Code.TimeSystem;
using TMPro;
using UnityEngine;

namespace PSW.Code.Payment
{
    public class PaymentDataPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private int oneDayPlusCoin = 1000;
        private int _dDayPaymentCoin;

        private bool _isNotPayment;

        private void Start()
        {
            _coinText.color = Color.red;
            Bus<PaymentTimeEvent>.OnEvent += StartPayment;
            Bus<OneDayTimeEvent>.OnEvent += OneDay;
        }

        private void StartPayment(PaymentTimeEvent evt)
        {
            _dDayPaymentCoin += oneDayPlusCoin;
            _isNotPayment = true;
        }

        private void OneDay(OneDayTimeEvent evt)
        {
            if(_isNotPayment)
            {
                GameOverEvent gameOver = new GameOverEvent();
                gameOver.D_Day = evt.Day;
                Bus<GameOverEvent>.Raise(gameOver);
            }
        }

        private void ChangeCoin(ChangeCoinEvent evt)
        {

        }


    }

    public struct GameOverEvent : IEvent
    {
        public int D_Day;
    }

    public struct GameWinEvent : IEvent { };
}