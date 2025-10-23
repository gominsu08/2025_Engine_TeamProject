using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using GMS.Code.UI;
using GMS.Code.UI.MainPanel;
using GMS.Code.Utill;
using PSW.Code.Container;
using PSW.Code.Sawtooth;
using System.Threading.Tasks;
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
            Bus<UIRefreshEvent>.OnEvent += RefreshUI;
            _completionSource = new AwaitableCompletionSource();
        }

        private void OnDestroy()
        {
            Bus<TileSelectEvent>.OnEvent -= HandleTileSelectEvent;
            Bus<TileUnSelectEvent>.OnEvent -= HandleTileUnSelectEvent;
            Bus<UIRefreshEvent>.OnEvent -= RefreshUI;
            Disable();
        }

        private void RefreshUI(UIRefreshEvent evt)
        {
            RefreshUI(evt.info);
        }

        private async void HandleTileUnSelectEvent(TileUnSelectEvent evt)
        {
            if ((_prevSelectTile != null && !(TileUtill.IsSame(evt.tileInfo, _prevSelectTile))) || evt.isBuy) return;
            await WaitPanel();
            _prevSelectTile = null;
        }

        private async Task WaitPanel(bool isLeft = false, TileInformation info = null, MachineType type = MachineType.None)
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
                PanelUp();
                _isWait = false;
            }
        }

        private void HandleTileSelectEvent(TileSelectEvent evt)
        {
            RefreshUI(evt.tileInfo);
        }

        public void RefreshUI(TileInformation info)
        {
            Disable();
            MachineType typeEnum = machineManager.IsMachineType(info);

            if(_isLeft == false)
            {
                if (typeEnum == MachineType.None)
                {
                    buildingMachinePanel.EnableForUI(info.item, info);
                }
                else if (typeEnum == MachineType.Brazier)
                {
                    //화로UI
                }
                else
                {
                    miningPanel.EnableForUI(info.item, info);
                }
            }

            //if (_prevSelectTile != null && !(TileUtill.IsSame(info, _prevSelectTile)))
            //{
            //    _prevSelectTile = info;
            //    return;
            //}
            //await WaitPanel(true, info, typeEnum);

            _prevSelectTile = info;
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

        public async void SawtoothButtonClick()
        {
            MachineType typeEnum = machineManager.IsMachineType(_prevSelectTile);
            await WaitPanel(_isLeft, _prevSelectTile, typeEnum);
        }

        private void PanelUp()
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
            {
                miningPanel.DisableUI();
                buildingMachinePanel.DisableUI();
            }

        }

        public void Disable()
        {
            miningPanel.DisableUI();
            buildingMachinePanel.DisableUI();
        }
    }
}