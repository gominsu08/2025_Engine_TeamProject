using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Core.System.Machines
{
    [Serializable]
    public class ItemAndValuePair
    {
        public ItemSO itemSO;
        public int value;
    }

    [CreateAssetMenu(fileName = "MachineData", menuName = "SO/GMS/System/Machine/MachineData")]
    public class MachineSO : ScriptableObject
    {
        public string machineName;
        public Tier machineTier;
        public MachineType machineType;
        public Sprite machineIcon;
        public Machine machinePrefab;
        public List<ItemAndValuePair> itemList = new List<ItemAndValuePair>();
        public bool isMoney;
        private int money = 1700;
        public string Description { get; private set; }

        public void OnValidate()
        {
            if (isMoney)
            {
                Description = string.Empty;
                Description += "1700원   ";
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