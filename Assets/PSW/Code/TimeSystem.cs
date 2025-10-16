using DG.Tweening;
using GMS.Code.Core;
using System;
using System.Collections;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] private float oneDayTime = 300f;
    [SerializeField] private float deliveryTime = 112.5f;
    [SerializeField] private Transform transformParent;

    private bool _isOneDelivery;
    private float _startTime;
    private TimeEvent _onTimeEvent;

    private SawtoothSystem sawtoothSystem;

    private void Start()
    {
        _startTime = Time.time;
        sawtoothSystem = GetComponent<SawtoothSystem>();
        sawtoothSystem.StartSawtooth(oneDayTime, true, transformParent);
    }

    private void Update()
    {
        if (Time.time - _startTime > deliveryTime && _isOneDelivery == false)
        {
            Bus<TimeEvent>.Raise(_onTimeEvent);
            _isOneDelivery = true;
        }
        else if(Time.time - _startTime > oneDayTime)
        {
            _startTime = Time.time;
            _isOneDelivery = false;
        }
    }
}
public struct TimeEvent : IEvent
{

}