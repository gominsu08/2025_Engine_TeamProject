using GMS.Code.Core;
using PSW.Code.Container;
using PSW.Code.TimeSystem;
using TMPro;
using UnityEngine;

namespace PSW.Code.Payment
{
    public class PaymentDataPanel : MonoBehaviour
    {
        [SerializeField] private ResourceContainer resourceContainer;
        [SerializeField] private TextMeshProUGUI _coinText;

        [SerializeField] private int lastDay = 7;
        [SerializeField] private int oneDayPlusCoin = 1000;

        [SerializeField] private Color notTargetCoinColor;

        private PaymentEndEvent paymentEndEvent;
        private GameWinEvent gameWinEvent;

        private int _dDayPaymentCoin;
        
        private bool _isNotPayment;
        private bool _isLastDay;

        private void Start()
        {
            Bus<PaymentTimeEvent>.OnEvent += StartPayment;
            Bus<OneDayTimeEvent>.OnEvent += OneDay;
            Bus<ChangeCoinEvent>.OnEvent += ChangeCoin;
        }

        private void OnDestroy()
        {
            Bus<PaymentTimeEvent>.OnEvent -= StartPayment;
            Bus<OneDayTimeEvent>.OnEvent -= OneDay;
            Bus<ChangeCoinEvent>.OnEvent -= ChangeCoin;
        }

        private void StartPayment(PaymentTimeEvent evt)
        {
            _dDayPaymentCoin += oneDayPlusCoin;
            SetTargetCoinText();
            _coinText.text = _dDayPaymentCoin.ToString();
            _isNotPayment = true;
        }

        private void OneDay(OneDayTimeEvent evt)
        {
            if (evt.Day >= lastDay) _isLastDay = true;

            if(_isNotPayment)
            {
                GameOverEvent gameOver = new GameOverEvent();
                gameOver.D_Day = evt.Day;
                Bus<GameOverEvent>.Raise(gameOver);
            }
        }

        private void ChangeCoin(ChangeCoinEvent evt)
        {
            SetTargetCoinText();
        }

        public void PaymentButtonClick()
        {
            if (resourceContainer.IsTargetCoin(_dDayPaymentCoin))
            {
                if(_isLastDay)
                    Bus<GameWinEvent>.Raise(gameWinEvent);
                
                resourceContainer.MinusCoin(_dDayPaymentCoin);
                Bus<PaymentEndEvent>.Raise(paymentEndEvent);
                _isNotPayment = false;
            }
        }

        private void SetTargetCoinText()
        {
            if (resourceContainer.IsTargetCoin(_dDayPaymentCoin))
                _coinText.color = Color.white;
            else
                _coinText.color = notTargetCoinColor;

        }
    }

    public struct GameOverEvent : IEvent
    {
        public int D_Day;
    }

    public struct GameWinEvent : IEvent { };
    public struct PaymentEndEvent : IEvent { };
}