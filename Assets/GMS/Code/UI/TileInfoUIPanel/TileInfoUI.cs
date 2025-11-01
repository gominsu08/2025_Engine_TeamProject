using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using GMS.Code.UI.Braziers;
using System;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class TileInfoUI : MonoBehaviour, IUIElement<TileInformation>
    {
        [SerializeField] private ResourcePanel resourcePanel;
        [SerializeField] private HasMachineTilePanel machineTilePanel;
        [SerializeField] private CenterTileInfoPanel centerTileInfoPanel;
        [SerializeField] private BrazierPanel brazierTilePanel;

        private RectTransform MyRect => transform as RectTransform;
        private TileManager _tileManager;
        private TileInformation _tileInfo;
        private MachineManager _machineManager;
        private Vector2 _startPoint;
        private bool _isDisable;


        public void Init(TileManager tileManager, MachineManager machineManager)
        {
            _tileManager = tileManager;
            _machineManager = machineManager;
            _startPoint = MyRect.anchoredPosition;

            machineTilePanel.Init(_tileManager, _machineManager);
            resourcePanel.Init(_tileManager);
            centerTileInfoPanel.Init(_tileManager);
            resourcePanel.DisableUI();

            Bus<UIRefreshEvent>.OnEvent += HandleRefreshEvent;
        }

        private void HandleRefreshEvent(UIRefreshEvent evt)
        {
            RefrashUI();
        }

        public void EnableForUI(TileInformation tileInfo)
        {
            if (_isDisable)
                MyRect.anchoredPosition = _startPoint;
            _isDisable = false;

            _tileInfo = tileInfo;
            RefrashUI();

            gameObject.SetActive(true);
        }

        private void RefrashUI()
        {
            MachineType type = _machineManager.IsMachineType(_tileInfo);
            if (type == MachineType.None)
            {
                if (_tileInfo.tileObject is CenterTile center)
                {
                    centerTileInfoPanel.EnableForUI(center);
                    resourcePanel.DisableUI();
                }
                else
                {
                    resourcePanel.EnableForUI(_tileInfo);
                    centerTileInfoPanel.DisableUI();
                }
                machineTilePanel.DisableUI();
                brazierTilePanel.DisableUI();
            }
            else
            {
                if (type == MachineType.Brazier)
                {
                    brazierTilePanel.EnableForUI(_machineManager.MachineContainer.GetMachintToTileInfo(_tileInfo) as Brazier);
                    machineTilePanel.DisableUI();
                }
                else
                {
                    machineTilePanel.EnableForUI(_tileInfo);
                    brazierTilePanel.DisableUI();
                }
                resourcePanel.DisableUI();
                centerTileInfoPanel.DisableUI();
            }
        }

        public void DisableUI()
        {
            gameObject.SetActive(false);

            resourcePanel.DisableUI();
            machineTilePanel.DisableUI();
            brazierTilePanel.DisableUI();
            centerTileInfoPanel.DisableUI();
            _isDisable = true;
        }
    }
}
