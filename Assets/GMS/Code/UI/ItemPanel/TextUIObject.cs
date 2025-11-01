using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace GMS.Code.UI.ItemPanel
{
    public class TextUIObject : MonoBehaviour, IUIElement<string>
    {
        [SerializeField] private TextMeshPro text;
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

        public void SetColor(Color color)
        {
            text.color = color;
        }
    }
}
