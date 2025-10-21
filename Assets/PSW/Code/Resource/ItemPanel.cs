using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using PSW.Code.Container;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PSW.Code.Resource
{
    public class ItemPanel : MonoBehaviour
    {
        [SerializeField] private ItemListSO itemListSO;
        [SerializeField] private ResourceContainer resourceContainer;
        private Dictionary<Tier, TierPanel> _panelDic = new Dictionary<Tier, TierPanel>();
        private void Start()
        {
            GetComponentsInChildren<TierPanel>().ToList().ForEach(v => _panelDic.Add(v.GetTier(), v));
            
            Dictionary<Tier, List<ItemSO>> itemListDic = new Dictionary<Tier, List<ItemSO>>();
            foreach(ItemSO item in itemListSO.itemSOList)
            {
                if (itemListDic.TryGetValue(item.tier, out List<ItemSO> itemList))
                    itemList.Add(item);
                else
                {
                    List<ItemSO> tempList = new();
                    tempList.Add(item);
                    itemListDic.Add(item.tier, tempList);
                }
            }


            for(int i = 1; i <= (int)Tier.ThirdTier; ++i)
                _panelDic[(Tier)i].Init(itemListDic[(Tier)i],resourceContainer);
        }
    }
        
}