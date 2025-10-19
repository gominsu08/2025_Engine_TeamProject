using GMS.Code.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Resource
{
    public class ItemTypePanel : MonoBehaviour
    {
        [SerializeField] private List<TypeButtonImageData> imageDataList;
        [SerializeField] private ItemListSO itemListSO;
        [SerializeField] private GameObject typeButtonPrefab;

        private Dictionary<ItemType, List<ItemSO>> itemListDic = new Dictionary<ItemType, List<ItemSO>>();

        private void Start()
        {
            foreach (ItemSO item in itemListSO.itemSOList)
            {
                if (itemListDic.TryGetValue(item.itemType, out List<ItemSO> itemList))
                    itemList.Add(item);
                else
                {
                    List<ItemSO> tempList = new();
                    tempList.Add(item);
                    itemListDic.Add(item.itemType, tempList);
                }
            }

            foreach (TypeButtonImageData data in imageDataList)
                Instantiate(typeButtonPrefab, transform)
                    .GetComponentInChildren<ItemTypeButton>()
                    .Init(data.TypeImage, itemListDic[data.Type]);

        }
    }

    [Serializable]
    public struct TypeButtonImageData
    {
        public ItemType Type;
        public Sprite TypeImage;
    }
}