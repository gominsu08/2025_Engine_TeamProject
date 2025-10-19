using System.Collections;
using TMPro;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class TextUI : MonoBehaviour, IUIElement<string>
    {
        [SerializeField] private TextMeshProUGUI text;

        public void DisableUI()
        {
            text.text = string.Empty;
            gameObject.SetActive(false);
        }

        public void EnableForUI(string t)
        {
            text.SetText(t);
            gameObject.SetActive(true);
        }
    }
}