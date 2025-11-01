using csiimnida.CSILib.SoundManager.RunTime;
using GMS.Code.Core;
using PSW.Code.Container;
using PSW.Code.TimeSystem;
using TMPro;
using UnityEngine;

namespace PSW.Code.Payment
{
    public class PaymentDataPanel : MonoBehaviour
    {
        [SerializeField] private PaymentPanel paymentPanel;
        [SerializeField] private ResourceContainer resourceContainer;
        [SerializeField] private TextMeshProUGUI _coinText;

        [SerializeField] private int lastDay = 7;
        [SerializeField] private int oneDayPlusCoin = 1000;

        [SerializeField] private Color notTargetCoinColor;

        private PaymentEndEvent paymentEndEvent;
        private GameWinEvent gameWinEvent;

        private int _dDayPaymentCoin;

        private string _soundName = "Payment";

        private bool _isAutoPayment;
        private bool _isNotPayment;
        private bool _isLastDay;

        private void Start()
        {
            Bus<PaymentTimeEvent>.OnEvent += StartPayment;
            Bus<OneDayTimeEvent>.OnEvent += OneDay;
            Bus<ChangeCoinEvent>.OnEvent += ChangeCoin;
            Bus<AutoPaymentToggle>.OnEvent += SetIsAutoPayment;
        }

        private void OnDestroy()
        {
            Bus<PaymentTimeEvent>.OnEvent -= StartPayment;
            Bus<OneDayTimeEvent>.OnEvent -= OneDay;
            Bus<ChangeCoinEvent>.OnEvent -= ChangeCoin;
            Bus<AutoPaymentToggle>.OnEvent -= SetIsAutoPayment;
        }

        private void SetIsAutoPayment(AutoPaymentToggle auto)
        {
            _isAutoPayment = auto.IsAuto;
        }

        private void StartPayment(PaymentTimeEvent evt)
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySound(_soundName);

            _dDayPaymentCoin += oneDayPlusCoin * evt.Day;
            SetTargetCoinText();
            _coinText.text = _dDayPaymentCoin.ToString();
            _coinText.ForceMeshUpdate();
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

        public void AutoPayment()
        {
            if (_isAutoPayment == false) return;

            PaymentButtonClick();
        }

        public void PaymentButtonClick()
        {
            if (paymentPanel.GetIsStopMove() == false) return;

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
            if (paymentPanel.GetIsStopMove() == false) return;

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