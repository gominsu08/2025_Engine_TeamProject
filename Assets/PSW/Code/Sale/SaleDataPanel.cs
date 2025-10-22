using DG.Tweening;
using GMS.Code.Items;
using PSW.Code.Container;
using PSW.Code.Sawtooth;
using UnityEngine;

namespace PSW.Code.Sale
{
    public class SaleDataPanel : MonoBehaviour
    {

        [SerializeField] private Vector2 popUpSize;
        [SerializeField] private float time;
        [SerializeField] private SawtoothSystem sawtoothSystem;

        private RectTransform _rectTransform;
        private bool _isLeft;

        public void PopUpDownPanel()
        {
            _isLeft = !_isLeft;
            if (sawtoothSystem.GetIsStopRotation() == false) return; 

            sawtoothSystem.StartSawtooth(time, _isLeft, transform.parent);
            _rectTransform.DOSizeDelta(_isLeft ? popUpSize : Vector2.zero, time);
            sawtoothSystem.SawtoothStop();
        }
    }
}