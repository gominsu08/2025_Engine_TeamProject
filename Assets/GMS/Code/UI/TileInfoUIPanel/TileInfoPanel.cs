using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using GMS.Code.Utill;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class TileInfoPanel : MonoBehaviour
    {
        [SerializeField] private TileInfoUI tileInfoUI;
        [SerializeField] private MachineManager machineManager;
        [SerializeField] private TileManager tileManager;

        private TileInformation _currentTileInfo;

        private void Awake()
        {
            tileInfoUI.Init(tileManager, machineManager);
            tileInfoUI.DisableUI();
            Bus<TileSelectEvent>.OnEvent += HandleTileSelectEvent;
            Bus<TileUnSelectEvent>.OnEvent += HandleTileUnSelectEvent;
        }

        private void OnDestroy()
        {
            Bus<TileSelectEvent>.OnEvent -= HandleTileSelectEvent;
            Bus<TileUnSelectEvent>.OnEvent -= HandleTileUnSelectEvent;
        }

        private void HandleTileSelectEvent(TileSelectEvent evt)
        {
            _currentTileInfo = evt.tileInfo;

            tileInfoUI.EnableForUI(_currentTileInfo);
        }

        private void HandleTileUnSelectEvent(TileUnSelectEvent evt)
        {
            if ((_currentTileInfo != null && !(TileUtill.IsSame(evt.tileInfo, _currentTileInfo))) || evt.isBuy) return;

            DisableAllUI();
        }

        public void DisableAllUI()
        {
            tileInfoUI.DisableUI();
        }
    }
}