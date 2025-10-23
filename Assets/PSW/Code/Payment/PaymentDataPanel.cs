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
        [SerializeField] private int oneDayPlusCoin = 1000;

        [SerializeField] private Color notTargetCoinColor;
        private int _dDayPaymentCoin;

        private bool _isNotPayment;

        private void Start()
        {
            Bus<PaymentTimeEvent>.OnEvent += StartPayment;
            Bus<OneDayTimeEvent>.OnEvent += OneDay;
            Bus<ChangeCoinEvent>.OnEvent += ChangeCoin;
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
}