using System;
using TMPro;
using UnityEngine;

namespace GMS.Code.UI
{
    [Serializable]
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
        private RectTransform _myRect => transform as RectTransform;
        private ToolBarUIData _data;
        private Vector2 _position;

        public void DisableUI()
        {
            textUI.SetText(string.Empty);
            gameObject.SetActive(false);
        }

        public void EnableForUI(Vector2 position, ToolBarUIData toolBarData)
        {
            _data = toolBarData;
            _position = position;
            TextSet();
            gameObject.SetActive(true);
        }

        public void TextSet()
        {
            textUI.SetText(_data.text);
            _myRect.anchoredPosition = _position;
            float lineCount = textUI.textInfo.lineCount;
            float textLenght = textUI.text.Length;
            _myRect.sizeDelta = new Vector2((textLenght) * 20f / 2 + 20 / lineCount, textUI.textInfo.lineCount * 60);
        }
    }
}
