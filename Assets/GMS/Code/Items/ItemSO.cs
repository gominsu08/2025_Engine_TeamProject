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

    }

}