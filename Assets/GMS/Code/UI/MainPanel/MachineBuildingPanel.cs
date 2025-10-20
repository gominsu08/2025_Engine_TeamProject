using Assets.GMS.Code.UI.MainPanel;
using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using PSW.Code.Container;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GMS.Code.UI.MainPanel
{
    public enum Tier
    {
        None,
        FirstTier,
        SecondTier,
        ThirdTier,
    }
    public class MachineBuildingPanel : MonoBehaviour, IUIElement<Tier, TileInformation>
    {
        private Tier _targetTier;
        private ToolBarUI _toolBarUI;
        private TileInformation _targetTileInfo;
        private ResourceContainer _container;

        [SerializeField] private MachineType machineType;
        [SerializeField] private List<BuildingMachineButtonUI> machineBuildingButton;

        public void Init(ToolBarUI toolBarUI, ResourceContainer container)
        {
            _toolBarUI = toolBarUI;
            _container = container;
            foreach (BuildingMachineButtonUI machineButton in machineBuildingButton)
                machineButton.Init(_toolBarUI);
        }

        public void DisableUI()
        {
            foreach (BuildingMachineButtonUI machineButton in machineBuildingButton)
                machineButton.DisableUI();

            gameObject.SetActive(false);
        }

        public void EnableForUI(Tier t, TileInformation tileInfo)
        {
            _targetTier = t;
            _targetTileInfo = tileInfo;

            foreach (BuildingMachineButtonUI machineButton in machineBuildingButton)
                machineButton.EnableForUI(HandleMachineBuild, _targetTier);
            gameObject.SetActive(true);

        }

        public void HandleMachineBuild(Tier tier, List<ItemAndValuePair> list)
        {
            bool isFail = false;

            if (list.Count != 0)
            {
                foreach (ItemAndValuePair item in list)
                {
                    if (!_container.IsTargetCountItem(item.itemSO, item.value))
                    {
                        isFail = true;
                    }
                }

                if (isFail == false)
                {
                    foreach (ItemAndValuePair item in list)
                    {
                        _container.MinusItem(item.itemSO, item.value);
                    }
                    Bus<MachineBuildingEvent>.Raise(new MachineBuildingEvent(tier, machineType, _targetTileInfo));
                    Bus<UIRefreshEvent>.Raise(new UIRefreshEvent(_targetTileInfo));
                }
            }
            else
            {
                if (!_container.IsTargetCoin(1700))
                {
                    isFail = true;
                }

                if (isFail == false)
                {
                    _container.MinusCoin(1700);
                    Bus<MachineBuildingEvent>.Raise(new MachineBuildingEvent(tier, machineType, _targetTileInfo));
                    Bus<UIRefreshEvent>.Raise(new UIRefreshEvent(_targetTileInfo));
                }
            }

            if (isFail)
            {
                //실패
            }
        }

        public MachineType GetMachineType() => machineType;
    }
}