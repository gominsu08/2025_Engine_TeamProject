using DG.Tweening;
using GMS.Code.Items;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PSW.Code.Resource
{
    public class ItemTypeButton : ItemButtonCompo
    {
        [SerializeField] private ItemButtonPanel buttonPanel;
        private List<ItemSO> _thisTypeList;

        public void Init(Sprite image, List<ItemSO> thisTypeList)
        {
            base.Init(image);
            buttonPanel.Init(thisTypeList);
        }

        public override void ButtonClick()
        {
            if (_isClickMoveStop == false) return;
            
            base.ButtonClick();

        }
    }
}