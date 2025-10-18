using TMPro;
using UnityEngine;

namespace GMS.Code.UI
{
    public struct ToolBarUIData
    {
        public string text;
        public ToolBarUIData(string text)
        {
            this.text = text;
        }
    }

    public class ToolBarUI : MonoBehaviour, IUIElement<Vector2, ToolBarUIData>
    {
        [SerializeField] private TextMeshProUGUI textUI;
        private ToolBarUIData _data;
        private Vector2 _position;

        public void DisableUI()
        {
            textUI.SetText(string.Empty);

        }

        public void EnableForUI(Vector2 position, ToolBarUIData toolBarData)
        {
            _data = toolBarData;
            _position = position;
            TextSet();
        }

        public void TextSet()
        {
            textUI.SetText(_data.text);
            textUI.rectTransform.anchoredPosition = _position;
            textUI.rectTransform.sizeDelta = new Vector2(textUI.text.Length * 20f, textUI.rectTransform.sizeDelta.y);
        }
    }
}
