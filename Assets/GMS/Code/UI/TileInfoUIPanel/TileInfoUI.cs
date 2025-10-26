using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using System;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class TileInfoUI : MonoBehaviour, IUIElement<TileInformation>
    {
        [SerializeField] private ResourcePanel resourcePanel;
        [SerializeField] private HasMachineTilePanel machineTilePanel;
        [SerializeField] private CenterTileInfoPanel centerTileInfoPanel;

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
            if (_machineManager.IsMachineType(_tileInfo) == MachineType.None)
            {
                if(_tileInfo.tileObject is CenterTile center)
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
            }
            else
            {
                machineTilePanel.EnableForUI(_tileInfo);
                resourcePanel.DisableUI();
                centerTileInfoPanel.DisableUI();
            }
        }

        public void DisableUI()
        {
            gameObject.SetActive(false);

            resourcePanel.DisableUI();
            machineTilePanel.DisableUI();
            _isDisable = true;
        }
    }
}
