using GMS.Code.UI.MainPanel;
using UnityEngine;
using UnityEngine.UI;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class MachinePanelChildUI : MonoBehaviour, IUIElement<bool, string>
    {
        [SerializeField] private Image myImage;
        [SerializeField] private TextUI text;
        [SerializeField] private Color disableColor;

        public void EnableForUI(bool isEnable, string name)
        {
            if (!isEnable)
            {
                myImage.color = disableColor;

                text.SetColor(disableColor);
            }
            else
            {
                text.SetColor(Color.white);
                myImage.color = Color.white;
            }

            text.EnableForUI(name);
            gameObject.SetActive(true);
        }

        public void SetColor(Color color)
        {
            myImage.color = color;
            text.SetColor(color);
        }

        public void DisableUI()
        {
            text.SetColor(Color.white);
            myImage.color = Color.white;
            text.DisableUI();
            //gameObject.SetActive(false);
        }
    }
}