using GMS.Code.Core;
using PSW.Code.Sawtooth;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GMS.Code.UI.PopUpUIs
{
    public class PopupUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private MouseInputSO mouseInputSO;
        [SerializeField] private Transform parent;
        [SerializeField] private SawtoothSystem sawtoothSystem;
        private RectTransform MyRect => transform as RectTransform;
        private bool _isDragging = false;
        private Vector2 _offset;

        public void OnPointerDown(PointerEventData eventData)
        {
            _offset = mouseInputSO.MousePosition - MyRect.anchoredPosition;
            _isDragging = true;
            parent.gameObject.SetActive(true);
            sawtoothSystem.StartSawtooth(5  , true, parent);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            sawtoothSystem.SawtoothStop();
        }

        public void Update()
        {
            if (_isDragging)
            {
                Vector2 screenSize = new Vector2(Screen.width, Screen.height);
                Vector2 newPos = mouseInputSO.MousePosition - _offset;
                newPos.x = Mathf.Clamp(newPos.x, 0, screenSize.x - MyRect.rect.width);
                newPos.y = Mathf.Clamp(newPos.y, 0, screenSize.y - MyRect.rect.height);
                MyRect.anchoredPosition = newPos;
            }
        }
    }
}