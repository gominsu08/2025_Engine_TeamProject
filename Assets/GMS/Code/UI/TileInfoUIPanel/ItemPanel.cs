using GMS.Code.UI.MainPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class ItemPanel : MonoBehaviour, IUIElement<string,int,Sprite>
    {
        [SerializeField] private TextUI text;
        [SerializeField] private IconUI icon;
        private const string ORANGE = "<color=orange>";
        private const string END_COLOR = "</color>";
        private const string END_SIZE = "</size>";
        private const string SIZE_17 = "<size=17>";
        private const string SIZE_15 = "<size=15>";

        public void DisableUI()
        {
            text.DisableUI();
            icon.DisableUI();
            Destroy(gameObject);
        }

        public void EnableForUI(string name,int value, Sprite icon)
        {
            this.icon.EnableForUI(icon);
            text.EnableForUI($"{name} {SIZE_17}/{END_SIZE} {ORANGE}{SIZE_15}{value}{END_SIZE}{END_COLOR}");
        }
    }
}
