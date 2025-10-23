using DG.Tweening;
using GMS.Code.Core;
using PSW.Code.Sawtooth;
using PSW.Code.TimeSystem;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace PSW.Code.Payment
{
    public class PaymentPanel : MonoBehaviour
    {
        [SerializeField] private SawtoothSystem sawtoothSystem;
        [SerializeField] private float popUpValue;
        [SerializeField] private float timeValue;

        private WaitForSeconds wait = new WaitForSeconds(0.5f);

        private float _moveValue;
        private float _targetMoveValue;
        private int _moveCount = 0;

        private void Start()
        {
            Bus<PaymentTimeEvent>.OnEvent += PopUp;
            Bus<PaymentEndEvent>.OnEvent += PopDown;
            _targetMoveValue = transform.localPosition.y;
            _moveValue = popUpValue / timeValue;
        }

        private void PopDown(PaymentEndEvent evt)
        {
            sawtoothSystem.StartSawtooth(timeValue, false, transform);
            StartCoroutine(PaymentPopUpTime(false));
        }

        private void PopUp(PaymentTimeEvent evt)
        {
            sawtoothSystem.StartSawtooth(timeValue,true,transform);
            StartCoroutine(PaymentPopUpTime(true));
        }

        private IEnumerator PaymentPopUpTime(bool _isUp)
        {
            if (_isUp == false)
            {
                _targetMoveValue += _moveValue;
                _moveCount--;
            }
            else
            {
                _targetMoveValue -= _moveValue;
                _moveCount++;
            }

            transform.DOLocalMoveY(_targetMoveValue, 0.5f);
            yield return wait;

            if (_moveCount < timeValue && _moveCount > 0)
                StartCoroutine(PaymentPopUpTime(_isUp));
            else
                sawtoothSystem.SawtoothStop(false);
        }


    }
}