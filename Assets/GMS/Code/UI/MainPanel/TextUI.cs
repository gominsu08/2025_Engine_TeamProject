using TMPro;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class TextUI : MonoBehaviour, IUIElement<string>
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string textString = string.Empty;

        public void DisableUI()
        {
            text.text = string.Empty;
            gameObject.SetActive(false);
        }

        public void EnableForUI(string t)
        {
            if (t == string.Empty)
                text.SetText(textString);
            else
                text.SetText(t);
            gameObject.SetActive(true);
        }

        public void  SetColor(Color color)
        {
            text.color = color;
        }
    }
}