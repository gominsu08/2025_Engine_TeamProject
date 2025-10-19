using DG.Tweening;
using GMS.Code.Items;
using System.Collections.Generic;
using UnityEngine;

namespace PSW.Code.Resource
{
    public class ItemButtonPanel : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private float popTime = 0.2f;

        public void Init(List<ItemSO> itemList)
        {
            foreach(ItemSO item in itemList)
            {
                Instantiate(buttonPrefab, transform)
                    .GetComponent<ItemButton>()
                    .Init(item.icon,this);
            }
        }

        public void PopUpDownPanel(bool isPopUp)
        {
            transform.DOScaleX(isPopUp ? 1 : 0, popTime);
        }
    }
}