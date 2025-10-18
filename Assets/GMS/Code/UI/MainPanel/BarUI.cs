using UnityEngine;
using UnityEngine.Events;

namespace GMS.Code.UI.MainPanel
{
    public class BarUI : MonoBehaviour, IUIElement<int>
    {
        [SerializeField] private RectTransform fillRect;
        private float _maxTime;

        public void EnableForUI(int maxTime)
        {
            _maxTime = maxTime;
            gameObject.SetActive(true);
        }

        public void UpdateUI(int currentTime)
        {
            float newWidth = currentTime / _maxTime;
            fillRect.sizeDelta = new Vector2(newWidth, fillRect.sizeDelta.y);
        }

        public void DisableUI()
        {
            fillRect.sizeDelta = new Vector2(0, fillRect.sizeDelta.y);
            _maxTime = 0;
            gameObject.SetActive(false);
        }
    }
}
