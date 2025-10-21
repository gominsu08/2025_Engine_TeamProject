using UnityEngine;
using UnityEngine.Events;

namespace GMS.Code.UI.MainPanel
{
    public class BarUI : MonoBehaviour, IUIElement<float, UnityAction>
    {
        [SerializeField] private RectTransform fillRect;
        private UnityAction _action;
        private float _maxTime;

        public void EnableForUI(float maxTime, UnityAction callback)
        {
            _maxTime = maxTime;
            gameObject.SetActive(true);
            _action = callback;
        }

        public void UpdateUI(float currentTime)
        {

            float newWidth = currentTime / _maxTime;
            if (newWidth >= 1 - Mathf.Epsilon)
                _action?.Invoke();
            fillRect.localScale = new Vector2(newWidth, 1);
        }

        public void DisableUI()
        {
            fillRect.sizeDelta = new Vector2(0, fillRect.sizeDelta.y);
            _maxTime = 0;
            gameObject.SetActive(false);
        }
    }
}
