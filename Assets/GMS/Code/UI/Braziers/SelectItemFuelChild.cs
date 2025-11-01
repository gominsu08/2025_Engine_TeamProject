using GMS.Code.Core.System.Machines;
using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using PSW.Code.Container;
using System;
using UnityEngine;

namespace GMS.Code.UI.Braziers
{
    public class SelectItemFuelChild : MonoBehaviour, IUIElement<ItemSO, Brazier, bool, Action<ItemSO>, ResourceContainer>
    {
        [SerializeField] private IconUI iconUI;
        [SerializeField] private IconUI buttonIcon;
        [SerializeField] private TextUI buttonText;
        [SerializeField] private TextUI nameText;
        [SerializeField] private TextUI descriptionText;
        [SerializeField] private TextUI countText;
        [SerializeField] private ButtonUI plusButton;
        [SerializeField] private Color notPlusColor;

        private Brazier _currentBrazier;
        private ItemSO _targetItem;
        private Action<ItemSO> action;
        private ResourceContainer _container;

        private void Awake()
        {
            plusButton.Init(null, true);
        }

        public void DisableUI()
        {
            Destroy(gameObject);
        }

        public void EnableForUI(ItemSO targetItem, Brazier brazier, bool isCanPlus, Action<ItemSO> callback, ResourceContainer container)
        {
            plusButton.DisableUI();

            _container = container;
            _targetItem = targetItem;
            _currentBrazier = brazier;
            action = callback;

            iconUI.EnableForUI(targetItem.icon);
            nameText.EnableForUI(targetItem.itemName);
            countText.EnableForUI($"보유량 : {_container.GetItemCount(_targetItem)}");
            descriptionText.EnableForUI($"연소량 : {targetItem.fuelAmount}");
            plusButton.EnableForUI(new ToolBarUIData(), HandleButtonClickEvent);
            Refresh(isCanPlus);
        }

        public void Refresh(bool isCanPlus)
        {
            if (isCanPlus)
            {
                iconUI.SetColor(Color.white);
                nameText.SetColor(Color.white);
                descriptionText.SetColor(Color.white);
                plusButton.SetColor(Color.white);
                buttonIcon.SetColor(Color.white);
                buttonText.SetColor(Color.white);
                plusButton.SetVisualDisable(false);
            }
            else
            {
                iconUI.SetColor(notPlusColor);
                buttonIcon.SetColor(notPlusColor);
                buttonText.SetColor(notPlusColor);
                nameText.SetColor(notPlusColor);
                descriptionText.SetColor(notPlusColor);
                plusButton.SetColor(notPlusColor);
                plusButton.SetVisualDisable(true);
            }
            countText.EnableForUI($"보유량 : {_container.GetItemCount(_targetItem)}");
        }

        private void HandleButtonClickEvent()
        {
            _currentBrazier.AddFuel(_targetItem);
            action?.Invoke(_targetItem);
        }
    }
}