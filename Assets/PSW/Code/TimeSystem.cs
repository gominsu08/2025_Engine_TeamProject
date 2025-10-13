using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.AI;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] private int oneDayTime;
    [SerializeField] private List<TimeEventData> timeEventDataList;

    private int _currentTime;
    private float _rotationValue;
    private Vector3 _rotationDir;
    private WaitForSeconds wait = new WaitForSeconds(1f);
    private int CurrentTime
    {
        get
        {
            return _currentTime;
        }
        set
        {
            _currentTime = value;
            _currentTime %= oneDayTime;
        }
    }

    private void Awake()
    {
        _rotationDir = transform.eulerAngles;
        _rotationValue = 360f / oneDayTime;
        StartCoroutine(SetTime());
    }

    public IEnumerator SetTime()
    {
        CurrentTime++;

        _rotationDir.z += _rotationValue;

        transform.DORotate(_rotationDir, 1f);

        foreach (TimeEventData data in timeEventDataList)
            data.IsInTime(CurrentTime);

        yield return wait;
        StartCoroutine(SetTime());
    }

}

[Serializable]
public struct TimeEventData
{
    public int EventTime;
    public UnityEvent OnTimeEvent;

    public void IsInTime(int time)
    {
        if (time == EventTime)
            OnTimeEvent?.Invoke();
    }
}