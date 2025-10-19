using System.Collections;
using UnityEngine;

namespace PSW.Code.Resource
{
    public class ItemButton : ItemButtonCompo
    {
        private ItemButtonPanel _parentPanel;

        public void Init(Sprite image, ItemButtonPanel panel)
        {
            base.Init(image);
            _parentPanel = panel;
        }

        public override void ButtonClick()
        {
            if (_isClickMoveStop == false) return;
            base.ButtonClick();
        }

        public override void ClickAnimEnd()
        { 
            _parentPanel.PopUpDownPanel(false);
        }
    }
}