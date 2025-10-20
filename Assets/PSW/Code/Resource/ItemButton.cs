using DG.Tweening;
using GMS.Code.Core;
using GMS.Code.Items;
using System.Collections;
using TMPro;
using UnityEngine;

namespace PSW.Code.Resource
{
    public class ItemButton : ItemButtonCompo
    {
        [SerializeField] private Transform namePanel;
        [SerializeField] private TextMeshProUGUI nameText;

        private ItemButtonPanel _parentPanel;
        private ItemButtonEvt itemButtonEvt;

        public void Init(ItemSO itemData,ItemButtonPanel panel)
        {
            base.Init(itemData.icon);
            _parentPanel = panel;
            nameText.text = itemData.itemName;
            itemButtonEvt = new ItemButtonEvt(itemData);
        }

        public override void ButtonClick()
        {
            if (_isClickMoveStop == false) return;
            base.ButtonClick();
        }

        public override void SetSize(bool isMouseUp)
        {
            namePanel.DOScaleX(isMouseUp ? 1 : 0, mouseUpDownTime);
            base.SetSize(isMouseUp);
        }

        public override void ClickAnimEnd()
        { 
            _parentPanel.PopUpDownPanel(false);
            Bus<ItemButtonEvt>.Raise(itemButtonEvt);
        }
    }
}

public struct ItemButtonEvt : IEvent
{
    public ItemSO ItemData;

    public ItemButtonEvt(ItemSO item)
    {
        ItemData = item;
    }



}