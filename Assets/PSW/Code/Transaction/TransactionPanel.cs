using DG.Tweening;
using PSW.Code.Sawtooth;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransactionPanel : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    [SerializeField] private Transform sawtoothTrm;
    [SerializeField] private Transform doorSawtoothTrm;

    [SerializeField] private SawtoothSystem doorSawtooth;
    [SerializeField] private SawtoothSystem rootSawtooth;

    private WaitForSeconds _wait = new WaitForSeconds(0.5f);

    private float _time;

    private float _openValue;
    private float _closeValue;

    private float _targetValue;
    private float _currentXValue;
    private int _count = 0;

    public void Init(float time, bool isStartOpen = false)
    {
        _time = time;
        _currentXValue = (isLeft ? -1 : 1) * 480;
        _openValue = _currentXValue;
        RectTransform rect = GetComponent<RectTransform>();
        _targetValue = rect.sizeDelta.x / time * (isLeft ? -1 : 1);
        _closeValue = _openValue + (_targetValue * time);
        print(_openValue);
        if (isStartOpen)
        {
            _count = (int)_time;
            transform.DOLocalMoveX(_closeValue, 0);
            _currentXValue = _closeValue;
        }
    }

    public void CloseTransaction(string loadSceneName)
    {
        _count = (int)_time;
        transform.DOLocalMoveX(_closeValue, 0);
        _currentXValue = _closeValue;

        StartCoroutine(PopPanel(false, loadSceneName));
        rootSawtooth.StartSawtooth(_time, false, sawtoothTrm);
    }

    public async void OpenTransaction()
    {
        _count = 0;
        transform.DOLocalMoveX(_openValue, 0);
        _currentXValue = _openValue;

        doorSawtooth.StartSawtooth(4, true, doorSawtoothTrm);
        await Awaitable.WaitForSecondsAsync(2f);
        doorSawtooth.SawtoothStop();

        StartCoroutine(PopPanel(true));
        rootSawtooth.StartSawtooth(_time, true, sawtoothTrm);
    }

    private IEnumerator PopPanel(bool isPopUp, string loadSceneName = "")
    {
        if (isPopUp == false)
        {
            _currentXValue -= _targetValue;
            _count--;
        }
        else
        {
            _currentXValue += _targetValue;
            _count++;
        }

        transform.DOLocalMoveX(_currentXValue, 0.5f);
        yield return _wait;

        if (_count < _time && _count > 0)
            StartCoroutine(PopPanel(isPopUp, loadSceneName));
        else
        {
            rootSawtooth.SawtoothStop(false);
            if (isPopUp == false)
            {
                doorSawtooth.StartSawtooth(4, false, doorSawtoothTrm);
                yield return new WaitForSeconds(2f);
                if (isLeft)
                {
                    print(loadSceneName);
                    SceneManager.LoadScene(loadSceneName);
                }
            }
        }
    }
}
