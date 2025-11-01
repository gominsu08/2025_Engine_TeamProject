using DG.Tweening;
using GMS.Code.Core;
using PSW.Code.Payment;
using PSW.Code.Sawtooth;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private Transaction transaction;
    [SerializeField] private SawtoothSystem rootSawtooth;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform sawtoothTrm;


    [SerializeField] private float targetY;
    [SerializeField] private float time;

    private float _moveTargetValue;
    private float _moveValue;
    private int _moveCount;
    private bool _gameEnd;

    private WaitForSeconds wait = new WaitForSeconds(0.5f);

    private void Start()
    {
        _moveValue = targetY / time;
        _moveTargetValue = transform.localPosition.y;
        Bus<GameOverEvent>.OnEvent += GameOver;
        Bus<GameWinEvent>.OnEvent += GameWin;
    }

    private void OnDestroy()
    {
        Bus<GameOverEvent>.OnEvent -= GameOver;
        Bus<GameWinEvent>.OnEvent -= GameWin;
    }

    private void GameOver(GameOverEvent evt)
    {
        if (_gameEnd) return;
        _gameEnd = true;

        text.text = "GameOver..";
        rootSawtooth.StartSawtooth(time, true, sawtoothTrm);
        StartCoroutine(MovePanel());
    }

    private void GameWin(GameWinEvent evt)
    {
        if (_gameEnd) return;
        _gameEnd = true;

        text.text = "GameWin!";
        rootSawtooth.StartSawtooth(time, true, sawtoothTrm);
        StartCoroutine(MovePanel());
    }

    public void RePlay()
    {
        transaction.FadeIn("MainScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator MovePanel()
    {
        _moveTargetValue += _moveValue;
        _moveCount++;

        transform.DOLocalMoveY(_moveTargetValue, 0.5f);
        yield return wait;

        if(_moveCount < time)
            StartCoroutine(MovePanel());
        else
            rootSawtooth.SawtoothStop(false);
    }

}
