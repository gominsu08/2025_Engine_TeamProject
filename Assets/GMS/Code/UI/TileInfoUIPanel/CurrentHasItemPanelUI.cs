using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class CurrentHasItemPanelUI : MonoBehaviour, IUIElement<List<ItemAndValuePair>>
    {
        [SerializeField] private ItemPanel prefab;
        [SerializeField] private RectTransform parent;
        private List<ItemPanel> panels = new List<ItemPanel>();

        public void DisableUI()
        {
            for(int i = 0; i < panels.Count; i++)
            {
                panels[i].DisableUI();
            }

            panels.Clear();
            gameObject.SetActive(false);
        }

        public void EnableForUI(List<ItemAndValuePair> value)
        {
            foreach (ItemAndValuePair item in value)
            {
                ItemPanel panel = Instantiate(prefab, parent);
                panel.EnableForUI(item.itemSO.itemName,item.value,item.itemSO.icon);
                parent.sizeDelta = new Vector2(parent.sizeDelta.x + 50,parent.sizeDelta.y);
                panels.Add(panel);
            }
            gameObject.SetActive(true);
        }
    }
}