using GMS.Code.Core.System.Machine;
using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using PSW.Code.Container;
using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class ResourceMiningPanel : MonoBehaviour, IUIElement<ItemSO, TileInformation>
    {
        [SerializeField] private BarUI barUI;
        [SerializeField] private ButtonUI destroyButtonUI;
        private bool _isEnabled = false;
        private TileInformation _tileInfo;
        private MachineManager _machineManager;
        private ToolBarUI _toolBarUI;

        public void Init(MachineManager machineManager, ToolBarUI toolBarUI)
        {
            _machineManager = machineManager;
            _toolBarUI = toolBarUI;
        }

        public void EnableForUI(ItemSO targetItem, TileInformation tileInfo)
        {
            _tileInfo = tileInfo;
            barUI.EnableForUI(_machineManager.GetMiningTime(_tileInfo));
            destroyButtonUI.EnableForUI(_toolBarUI, new ToolBarUIData()
            {
                text = "기계를 철거 하시겠습니까?"
            }, HandleDestroyMachine);
            gameObject.SetActive(true);
            _isEnabled = true;
        }

        private void HandleDestroyMachine()
        {

        }

        public void Update()
        {
            if (_isEnabled)
                barUI.UpdateUI(_machineManager.GetMiningTime(_tileInfo));
        }

        public void DisableUI()
        {
            barUI.DisableUI();
            destroyButtonUI.DisableUI();
            gameObject.SetActive(false);
            _isEnabled = false;
        }
    }
}
