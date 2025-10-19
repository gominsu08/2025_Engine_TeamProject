using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machine;
using GMS.Code.Core.System.Maps;
using GMS.Code.UI;
using GMS.Code.UI.MainPanel;
using GMS.Code.Utill;
using PSW.Code.Container;
using PSW.Code.Sawtooth;
using UnityEngine;

namespace PSW.Code.Make
{
    public enum UIType
    {
        Mining,
        Create,
        /// <summary>
        /// 화로
        /// </summary>
        Brazier
    }

    public class SawtoothAndPanelSystem : MonoBehaviour
    {
        [SerializeField] private SawtoothSystem sawtoothSystem;
        [SerializeField] private MachineManager machineManager;
        [SerializeField] private ResourceContainer container;
        [SerializeField] private MakePanel makePanel;

        [Header("Panels")]
        [SerializeField] private ResourceMiningPanel miningPanel;
        [SerializeField] private BuildingMachinePanelContainer buildingMachinePanel;
        [SerializeField] private ToolBarUI toolBarUI;

        [Header("Sawtooth")]
        [SerializeField] private float rotationTime;
        [SerializeField] private Transform parentTransform;

        private bool _isLeft = true;
        private bool _isWait;
        private TileInformation _prevSelectTile;
        private AwaitableCompletionSource _completionSource;

        private void Awake()
        {
            miningPanel.Init(machineManager, toolBarUI);
            buildingMachinePanel.Init(toolBarUI, container);
            Bus<TileSelectEvent>.OnEvent += HandleTileSelectEvent;
            Bus<TileUnSelectEvent>.OnEvent += HandleTileUnSelectEvent;
            _completionSource = new AwaitableCompletionSource();
        }

        private async void HandleTileUnSelectEvent(TileUnSelectEvent evt)
        {
            if ((_prevSelectTile != null && !(TileUtill.IsSame(evt.tileInfo, _prevSelectTile))) || evt.isBuy) return;
            await WaitPanel();
            _prevSelectTile = null;
        }

        private async System.Threading.Tasks.Task WaitPanel(bool isLeft = false, TileInformation info = null, MachineType type = MachineType.None)
        {

            _isWait = true;
            if (_isLeft == isLeft)
            {
                await _completionSource.Awaitable;
                if (info != null)
                {
                    if (type == MachineType.None)
                        buildingMachinePanel.EnableForUI(info.item, info);
                    else
                        miningPanel.EnableForUI(info.item, info);
                }
                SawtoothButtonClick();
                _isWait = false;
            }
        }

        private async void HandleTileSelectEvent(TileSelectEvent evt)
        {
            MachineType typeEnum = machineManager.IsMachineType(evt.tileInfo);

            if (typeEnum == MachineType.None)
            {
                buildingMachinePanel.EnableForUI(evt.tileInfo.item, evt.tileInfo);
            }
            else if (typeEnum == MachineType.Brazier)
            {
                //화로UI
            }
            else
            {
                miningPanel.EnableForUI(evt.tileInfo.item, evt.tileInfo);
            }

            if (_prevSelectTile != null && !(TileUtill.IsSame(evt.tileInfo, _prevSelectTile)))
            {
                _prevSelectTile = evt.tileInfo;
                return;
            }
            await WaitPanel(true, evt.tileInfo);

            _prevSelectTile = evt.tileInfo;
        }

        public void Update()
        {
            if (_completionSource != null && _isWait)
            {
                if (sawtoothSystem.GetIsStopRotation() && makePanel.GetIsStopMove())
                {
                    _completionSource.SetResult();
                    _completionSource.Reset();
                }
            }
        }

        public void SawtoothButtonClick()
        {
            if (sawtoothSystem.GetIsStopRotation() && makePanel.GetIsStopMove())
            {
                sawtoothSystem.StartSawtooth(rotationTime, _isLeft, parentTransform);
                _isLeft = !_isLeft;
                makePanel.StartPopPanel();
            }

        }

        public void DisableAllUI()
        {
            if (_isLeft)
                miningPanel.DisableUI();

        }
    }
}