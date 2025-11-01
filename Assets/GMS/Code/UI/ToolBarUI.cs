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
        private RectTransform MyRect => transform as RectTransform;
        private ToolBarUIData _data;
        private Vector2 _position;

        public void DisableUI()
        {
            textUI.SetText(string.Empty);
            MyRect.sizeDelta = new Vector2(200,60);
            _position = Vector2.zero;
            _data = new ToolBarUIData(string.Empty  );
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
            textUI.UpdateMeshPadding();
            MyRect.anchoredPosition = _position;
            float lineCount = textUI.textInfo.lineCount;
            float textLenght = textUI.text.Length;
            MyRect.sizeDelta = new Vector2((textLenght) * 20f + 20, 60);
        }
    }
}
