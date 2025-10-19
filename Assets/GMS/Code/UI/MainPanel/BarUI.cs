using UnityEngine;
using UnityEngine.Events;

namespace GMS.Code.UI.MainPanel
{
    public class BarUI : MonoBehaviour, IUIElement<float>
    {
        [SerializeField] private RectTransform fillRect;
        private float _maxTime;

        public void EnableForUI(float maxTime)
        {
            _maxTime = maxTime;
            gameObject.SetActive(true);
        }

        public void UpdateUI(float currentTime)
        {
            float newWidth = currentTime / _maxTime;
            Debug.Log("UPDATE : " + newWidth);
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
