using DG.Tweening;
using GMS.Code.Items;
using PSW.Code.Container;
using PSW.Code.Sawtooth;
using System.Threading.Tasks;
using UnityEngine;

namespace PSW.Code.Sale
{
    public class PopPanel : MonoBehaviour
    {
        [SerializeField] private Vector2 popUpSize;
        [SerializeField] private float time;
        [SerializeField] protected SawtoothSystem sawtoothSystem;

        private RectTransform _rectTransform;
        private bool _isLeft;

        public void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public async void PopUpDownPanel()
        {
            _isLeft = !_isLeft;
            if (sawtoothSystem.GetIsStopRotation() == false) return; 

            sawtoothSystem.StartSawtooth(time, _isLeft, transform.parent);
            _rectTransform.DOSizeDelta(_isLeft ? popUpSize : Vector2.zero, time);
            await Awaitable.WaitForSecondsAsync(time);
            sawtoothSystem.SawtoothStop(false);
        }
    }
}