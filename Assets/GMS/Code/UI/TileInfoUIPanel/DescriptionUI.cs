using GMS.Code.UI.MainPanel;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class DescriptionUI : MonoBehaviour, IUIElement<string>
    {
        [SerializeField] private TextUI text;

        public void DisableUI()
        {
            text.DisableUI();
            gameObject.SetActive(false);
        }

        public void EnableForUI(string description)
        {
            gameObject.SetActive(true);
            text.EnableForUI(description);
        }
    }
}