using GMS.Code.Core.System.Machine;
using GMS.Code.UI.MainPanel;
using UnityEngine;

namespace GMS.Code.Items
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "SO/GMS/Item", order = 0)]
    public class ItemSO : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public bool isStackable = true;
        public int maxStack = 99;
        public int sellMoney;
        public ItemType itemType;
        public MachineType machineType;
        public Tier tier;
    }

    public enum ItemType
    {
        Tree,
        Mineral,
        Soil,
        Fuel,
        Process
    }

}