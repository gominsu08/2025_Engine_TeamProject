using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public class Storage : MonoBehaviour
    {
        public static Storage Instance { get; private set; }

        private Dictionary<ItemSO,int> carryingItems = new Dictionary<ItemSO,int>();

        public void Init()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;

            Bus<StorageItemAddEvent>.OnEvent += HandleAddItem;
        }

        private void OnDestroy()
        {
            Bus<StorageItemAddEvent>.OnEvent -= HandleAddItem;
        }

        private void HandleAddItem(StorageItemAddEvent evt)
        {
            AddItem(evt.Item,evt.Value);
            Bus<CenterTilePanelRefresh>.Raise(new CenterTilePanelRefresh());
        }

        public void AddItem(ItemSO item, int value)
        {
            if(carryingItems.ContainsKey(item))
                carryingItems[item] += value;
            else
                carryingItems.Add(item,value);
        }

        public List<ItemAndValuePair> TakeItemVlaue()
        {
            List<ItemAndValuePair> list = GetInfoItemValue();
            carryingItems.Clear();
            return list;
        }

        public List<ItemAndValuePair> GetInfoItemValue()
        {
            List<ItemAndValuePair> list = new List<ItemAndValuePair>();

            foreach (ItemSO item in carryingItems.Keys)
            {
                list.Add(new ItemAndValuePair(item, carryingItems[item]));
            }

            return list;
        }


    }
}
