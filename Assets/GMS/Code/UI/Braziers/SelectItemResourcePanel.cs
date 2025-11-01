using GMS.Code.Core;
using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using NUnit.Framework;
using PSW.Code.Container;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.UI.Braziers
{
    [Serializable]
    public class ProcessItemPair
    {
        public ItemSO resourceItem;
        public ItemSO makeItem;

        public ProcessItemPair(ItemSO resourceItem, ItemSO makeItem)
        {
            this.resourceItem = resourceItem;
            this.makeItem = makeItem;
        }
    }

    public class SelectItemResourcePanel : MonoBehaviour, IUIElement<Action<ItemSO, ItemSO>>
    {
        [SerializeField] private SelectItemResourceChild childPrefab;
        [SerializeField] private List<ProcessItemPair> Piers = new List<ProcessItemPair>();
        [SerializeField] private RectTransform parent;
        [SerializeField] private ResourceContainer container;
        private List<SelectItemResourceChild> _childs = new List<SelectItemResourceChild>();

        private Action<ItemSO, ItemSO> action;

        public void DisableUI()
        {
            
            ClearChild();
            gameObject.SetActive(false);
        }

        private void ClearChild()
        {
            Bus<ChangeItem>.OnEvent -= HandleChangeItemEvent;
            for (int i = _childs.Count - 1; i >= 0; i--)
            {
                _childs[i].DisableUI();
            }
            _childs.Clear();
        }

        public void EnableForUI(Action<ItemSO, ItemSO> callback)
        {
            float parentSize = 0;
            action = callback;
            ClearChild();

            foreach (ProcessItemPair itemPair in Piers)
            {
                SelectItemResourceChild newChild = Instantiate(childPrefab, transform);
                bool isCanSelect = container.IsTargetCountItem(itemPair.resourceItem,1);
                newChild.EnableForUI(callback, itemPair, isCanSelect,container);
                _childs.Add(newChild);
                parentSize += 50;
            }
            parent.sizeDelta = new Vector2(parent.sizeDelta.x, parentSize);
            Bus<ChangeItem>.OnEvent += HandleChangeItemEvent;
            gameObject.SetActive(true);
        }

        private void HandleChangeItemEvent(ChangeItem evt)
        {
            RefreshUI();
        }

        public void RefreshUI()
        {
            for (int i = _childs.Count - 1; i >= 0; i--)
            {
                bool isCanSelect = container.IsTargetCountItem(Piers[i].resourceItem, 1);
                Debug.Log(isCanSelect + $" {Piers[i].resourceItem.itemName}");
                _childs[i].EnableForUI(action, Piers[i], isCanSelect, container);
            }
        }
    }
}