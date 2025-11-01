using GMS.Code.Core;
using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using PSW.Code.Container;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.UI.Braziers
{
    public class SelectItemFuelPanel : MonoBehaviour, IUIElement<Brazier>
    {
        private Brazier _currentBrazier;
        private List<SelectItemFuelChild> _childs = new List<SelectItemFuelChild>();

        [SerializeField] private SelectItemFuelChild childPrefab;
        [SerializeField] private List<ItemSO> fuelItems = new List<ItemSO>();
        [SerializeField] private RectTransform parent;
        [SerializeField] private ResourceContainer container;


        public void DisableUI()
        {
            ClearChild();
            gameObject.SetActive(false);
        }

        private void ClearChild()
        {
            for (int i = _childs.Count - 1; i >= 0; i--)
            {
                _childs[i].DisableUI();
            }

            _childs.Clear();
        }

        public void EnableForUI(Brazier brazier)
        {
            float parentSize = 0;
            _currentBrazier = brazier;
            ClearChild();

            foreach (ItemSO item in fuelItems)
            {
                SelectItemFuelChild newChild = Instantiate(childPrefab, transform);
                bool isCanPlus = container.IsTargetCountItem(item, 1);
                newChild.EnableForUI(item, _currentBrazier, isCanPlus, HandleResourceMinus, container);
                _childs.Add(newChild);
                parentSize += 50;
            }
            parent.sizeDelta = new Vector2(parent.sizeDelta.x, parentSize);
            Bus<ChangeItem>.OnEvent += HandleChangeItemEvent;
            gameObject.SetActive(true);
        }

        private void HandleResourceMinus(ItemSO target)
        {
            container.MinusItem(target, 1);
        }

        private void HandleChangeItemEvent(ChangeItem evt)
        {
            RefreshUI();
        }

        public void RefreshUI()
        {
            for (int i = _childs.Count - 1; i >= 0; i--)
            {
                bool isCanSelect = container.IsTargetCountItem(fuelItems[i], 1);
                _childs[i].EnableForUI(fuelItems[i], _currentBrazier, isCanSelect, HandleResourceMinus, container);
            }
        }
    }
}