using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class MachinePanelUI : MonoBehaviour, IUIElement<ItemSO>
    {
        private ItemSO item;
        [SerializeField] private List<MachinePanelChildUI> child = new List<MachinePanelChildUI>();
        [SerializeField] private List<MachineSO> machineSOs = new List<MachineSO>();
        private Dictionary<MachineType, List<MachineSO>> machines = new Dictionary<MachineType, List<MachineSO>>();

        public void Init()
        {
            foreach (MachineSO item in machineSOs)
            {
                if (!machines.ContainsKey(item.machineType))
                {
                    machines.Add(item.machineType, new List<MachineSO>() { item });
                }
                else
                {
                    machines[item.machineType].Add(item);
                }
            }
        }

        public void EnableForUI(ItemSO item)
        {
            this.item = item;

            for (int i = 0; i < child.Count; i++)
            {
                if (machines[item.machineType].Count > i)
                {
                    if ((int)item.tier <= (int)machines[item.machineType][i].machineTier)
                        child[i].EnableForUI(true, machines[item.machineType][i].machineName);
                    else
                        child[i].EnableForUI(false, machines[item.machineType][i].machineName);
                }
                else
                    child[i].DisableUI();
            }

            gameObject.SetActive(true);
        }

        public void DisableUI()
        {
            gameObject.SetActive(false);
        }

        public void NoneItem()
        {
            for (int i = 0; i < child.Count; i++)
            {
                if (i == 0)
                {
                    child[i].EnableForUI(true,"È­·Î Lv.1");
                }
                else
                    child[i].DisableUI();
            }
            gameObject.SetActive(true);
        }


    }
}