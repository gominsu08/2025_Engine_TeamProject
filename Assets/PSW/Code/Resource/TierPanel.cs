using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using PSW.Code.Container;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Resource
{
    public class TierPanel : MonoBehaviour
    {
        [SerializeField] private Tier tierType;
        [SerializeField] private Transform itemDataListTrm;
        [SerializeField] private GameObject itemDataPrefab;

        public Tier GetTier() => tierType;

        public void Init(List<ItemSO> thisTypeitemList, ResourceContainer container)
        {
            foreach(ItemSO item in thisTypeitemList)
            {
                Instantiate(itemDataPrefab, itemDataListTrm)
                    .GetComponentInChildren<ItemData>()
                    .Init(item,container.GetItemCount(item));
            }
        }
    }
}