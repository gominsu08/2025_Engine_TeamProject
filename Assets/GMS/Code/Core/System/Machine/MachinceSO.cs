using GMS.Code.Core.System.Machine;
using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GMS.Code.Core.System.Machine
{
    [Serializable]
    public class ItemAndValuePair
    {
        public ItemSO itemSO;
        public int value;
    }

    [CreateAssetMenu(fileName = "MachineData", menuName = "SO/GMS/System/Machine/MachineData")]
    public class MachinceSO : ScriptableObject
    {
        public string machineName;
        public Tier machineTier;
        public MachineType machineType;
        public Sprite machineIcon;
        public List<ItemAndValuePair> itemList = new List<ItemAndValuePair>();
        public string Description { get; private set; }
        public bool isMoney;
        private int money = 1700;

        public void OnValidate()
        {
            if (isMoney)
            {
                Description = string.Empty;
                Description += "1700원";
                return;
            }

            if (itemList.Count > 0)
            {
                Description = string.Empty;
                foreach (ItemAndValuePair item in itemList)
                {
                    if (item.itemSO == null || item.value == 0) return;
                    Description += $"{item.itemSO.itemName} : {item.value}개 | ";
                }
            }
        }

    }
}