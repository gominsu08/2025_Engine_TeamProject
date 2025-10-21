using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using PSW.Code.Container;
using PSW.Code.Sawtooth;
using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class ResourceMiningPanel : MonoBehaviour, IUIElement<ItemSO, TileInformation>
    {
        [SerializeField] private BarUI barUI;
        [SerializeField] private ButtonUI destroyButtonUI;
        [SerializeField] private TextUI nameTextUI;
        [SerializeField] private IconUI iconUI;
        [SerializeField] private SawtoothSystem sawtoothSystem;
        [SerializeField] private Transform parentTrm;
        private bool _isEnabled = false;
        private TileInformation _tileInfo;
        private MachineManager _machineManager;
        private ToolBarUI _toolBarUI;
        private ItemSO _targetItem;

        public void Init(MachineManager machineManager, ToolBarUI toolBarUI)
        {
            _machineManager = machineManager;
            _toolBarUI = toolBarUI;
        }

        public void EnableForUI(ItemSO targetItem, TileInformation tileInfo)
        {
            _targetItem = targetItem;
            _tileInfo = tileInfo;
            barUI.EnableForUI(5, HandleMiningEnd);
            
            destroyButtonUI.Init(_toolBarUI);
            destroyButtonUI.EnableForUI(new ToolBarUIData()
            {
                text = "기계를 철거 하시겠습니까?"
            }, HandleDestroyMachine);
            gameObject.SetActive(true);
            RefreshUI();
            _isEnabled = true;
        }

        public void RefreshUI()
        {
            sawtoothSystem.StartSawtooth(5, true, parentTrm);
            nameTextUI.EnableForUI(_targetItem.itemName);
            iconUI.EnableForUI(_targetItem.icon);
        }

        private void HandleDestroyMachine()
        {
            _machineManager.DestroyMachine(_tileInfo);
            DisableUI();
            Bus<TileUseUnSelectEvent>.Raise(new TileUseUnSelectEvent(_tileInfo));
        }

        public void Update()
        {
            if (_isEnabled)
                barUI.UpdateUI(_machineManager.GetCurrentMiningTime(_tileInfo));
        }

        private void HandleMiningEnd()
        {
            sawtoothSystem.StartSawtooth(5,true, parentTrm);
        }

        public void DisableUI()
        {
            barUI.DisableUI();
            nameTextUI.DisableUI();
            iconUI.DisableUI();
            destroyButtonUI.DisableUI();
            gameObject.SetActive(false);
            sawtoothSystem.SawtoothStop();
            _isEnabled = false;
        }
    }
}
