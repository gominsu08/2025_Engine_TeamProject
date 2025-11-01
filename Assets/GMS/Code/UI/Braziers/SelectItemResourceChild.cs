using GMS.Code.Items;
using GMS.Code.UI.MainPanel;
using PSW.Code.Container;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMS.Code.UI.Braziers
{
    public class SelectItemResourceChild : MonoBehaviour, IUIElement<Action<ItemSO, ItemSO>, ProcessItemPair, bool, ResourceContainer>
    {
        [SerializeField] private ButtonUI selectButton;
        [SerializeField] private IconUI buttonIcon;
        [SerializeField] private TextUI buttonText;
        [SerializeField] private TextUI nameText;
        [SerializeField] private TextUI descriptionText;
        [SerializeField] private IconUI iconUI;
        [SerializeField] private Color notSelectColor;
        [SerializeField] private Image panelImage;

        private ItemSO _targetItem;
        private ItemSO _resourceItem;
        private ResourceContainer _container;

        public void DisableUI()
        {
            selectButton.DisableUI();
            nameText.DisableUI();
            descriptionText.DisableUI();
            iconUI.DisableUI();
            Destroy(gameObject);
        }

        private void Awake()
        {
            selectButton.Init(null, true);
        }

        public void EnableForUI(Action<ItemSO, ItemSO> callback, ProcessItemPair pair, bool isCanSelect, ResourceContainer container)
        {
            selectButton.DisableUI();

            _container = container;
            _targetItem = pair.makeItem;
            _resourceItem = pair.resourceItem;

            selectButton.EnableForUI(new ToolBarUIData(), () => callback?.Invoke(_targetItem, _resourceItem));
            nameText.EnableForUI(_targetItem.itemName);

                string description;
            if (!isCanSelect)
                description = $"{_resourceItem.itemName}이(가) 필요합니다.";
            else
            {

                description = $"재료보유량 : {container.GetItemCount(pair.resourceItem)}";
            }
                descriptionText.EnableForUI(description);
            iconUI.EnableForUI(_targetItem.icon);

            if (isCanSelect == false)
            {
                iconUI.SetColor(notSelectColor);
                panelImage.color = notSelectColor;
                nameText.SetColor(notSelectColor);
                descriptionText.SetColor(notSelectColor);
                selectButton.SetColor(notSelectColor);
                buttonIcon.SetColor(notSelectColor);
                buttonText.SetColor(notSelectColor);
                selectButton.SetVisualDisable();
            }
            else
            {
                iconUI.SetColor(Color.white);
                panelImage.color = Color.white;
                nameText.SetColor(Color.white);
                descriptionText.SetColor(Color.white);
                selectButton.SetColor(Color.white);
                buttonIcon.SetColor(Color.white);
                buttonText.SetColor(Color.white);
                selectButton.SetVisualDisable(false);
            }

            gameObject.SetActive(true);
        }
    }
}