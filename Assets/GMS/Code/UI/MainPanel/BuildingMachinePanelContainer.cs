using GMS.Code.Core.System.Machine;
using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using PSW.Code.Container;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace GMS.Code.UI.MainPanel
{

    public class BuildingMachinePanelContainer : MonoBehaviour, IUIElement<ItemSO, TileInformation>
    {
        private ItemSO _itemSO;
        private TileInformation _targetTileInfo;
        private ToolBarUI _toolBarUI;
        private MachineBuildingPanel _targetPanel;
        private ResourceContainer _container;
        [SerializeField] private List<MachineBuildingPanel> panels = new List<MachineBuildingPanel>();

        public void Init(ToolBarUI toolBarUI, ResourceContainer container)
        {
            _toolBarUI = toolBarUI;
            _container = container;

            foreach (var panel in panels)
                panel.Init(_toolBarUI, _container);
        }

        public void DisableUI()
        {
            foreach (var panel in panels)
                panel.DisableUI();

            _targetPanel = null;
            _targetTileInfo = null;
            gameObject.SetActive(false);
        }

        public void EnableForUI(ItemSO targetItem, TileInformation targetTileInfo)
        {
            _targetTileInfo = targetTileInfo;


            if (targetItem != null)
            {
                _itemSO = targetItem;
                _targetPanel = GetPanel(targetItem.machineType);
                foreach (var panel in RemainPanel(targetItem.machineType))
                    panel.DisableUI();
                _targetPanel.EnableForUI(_itemSO.tier, _targetTileInfo);
            }
            else
            {
                _targetPanel = GetPanel(MachineType.Brazier);
                _targetPanel.EnableForUI(Tier.None, _targetTileInfo);
                foreach (var panel in RemainPanel(MachineType.Brazier))
                    panel.DisableUI();
            }



                gameObject.SetActive(true);
        }

        public MachineBuildingPanel GetPanel(MachineType machineType)
        {
            MachineBuildingPanel result = null;

            foreach (MachineBuildingPanel panel in panels)
            {
                if (panel.GetMachineType() == machineType)
                    result = panel;
            }

            return result;
        }

        public List<MachineBuildingPanel> RemainPanel(MachineType machineType)
        {
            List<MachineBuildingPanel> result = panels.ToList();

            foreach (MachineBuildingPanel panel in panels)
            {
                if (panel.GetMachineType() == machineType)
                    result.Remove(panel);
            }

            return result;
        }


    }
}