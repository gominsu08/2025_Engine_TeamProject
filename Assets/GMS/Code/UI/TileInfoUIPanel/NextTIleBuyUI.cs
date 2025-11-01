using GMS.Code.UI.MainPanel;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class NextTIleBuyUI : MonoBehaviour, IUIElement<int>
    {
        [SerializeField] private TextUI text;

        public void DisableUI()
        {
            text.DisableUI();
            gameObject.SetActive(false);
        }

        public void EnableForUI(int amount)
        {
            gameObject.SetActive(true);
            text.EnableForUI($"다음 타일구매 : <#FAFF00>{amount}");
        }

        

    }
}