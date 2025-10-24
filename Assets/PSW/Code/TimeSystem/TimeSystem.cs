using PSW.Code.Sawtooth;
using GMS.Code.Core;
using UnityEngine;
using TMPro;

namespace PSW.Code.TimeSystem
{
    public class TimeSystem : MonoBehaviour
    {
        [SerializeField] private float oneDayTime = 300f;
        [SerializeField] private float deliveryTime = 112.5f;
        [SerializeField] private Transform transformParent;

        [SerializeField] private float minLightXValue;
        [SerializeField] private Light directionalLight;

        private bool _isMorning = true; 
        private int _day = 1;

        private bool _isOneDelivery;
        private float _startTime;
        private float _currentTime;

        private PaymentTimeEvent _onTimeEvent;
        private OneDayTimeEvent _oneDayTimeEvent;

        private SawtoothSystem sawtoothSystem;

        private void Start()
        {
            _startTime = Time.time;
            sawtoothSystem = GetComponent<SawtoothSystem>();
            sawtoothSystem.StartSawtooth(oneDayTime * 2, true, transformParent);
        }

        private void Update()
        {
            _currentTime = Time.time - _startTime;
            if (_currentTime > deliveryTime && _isOneDelivery == false)
            {
                _onTimeEvent.Day = _day;
                Bus<PaymentTimeEvent>.Raise(_onTimeEvent);
                _isOneDelivery = true;
            }
            else if (_currentTime > oneDayTime)
            {
                _oneDayTimeEvent.Day = ++_day;
                Bus<OneDayTimeEvent>.Raise(_oneDayTimeEvent);
                print("하루 지남");
                _startTime = Time.time;
                _isOneDelivery = false;
            }
        }

        public void SetDirectionalLight()
        {
            //if()
        }
    }
    public struct PaymentTimeEvent : IEvent { public int Day; }
    public struct OneDayTimeEvent : IEvent { public int Day; }

}