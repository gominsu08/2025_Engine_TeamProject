using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Maps;
using System;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class CenterTileInfoPanel : MonoBehaviour, IUIElement<CenterTile>
    {
        [SerializeField] private HeadQuarterCallButton mHeadQuarterCallButton;
        [SerializeField] private CurrentHasItemPanelUI currentHasItemPanelUI;
        [SerializeField] private NextTIleBuyUI nextTIleBuyUI;

        private TileManager _tileManager;
        private CenterTile _tile;

        public void Init(TileManager tileManager)
        {
            _tileManager = tileManager;
        }

        public void DisableUI()
        {
            currentHasItemPanelUI.DisableUI();
            nextTIleBuyUI.DisableUI();
            mHeadQuarterCallButton.DisableUI();
            Bus<CenterTilePanelRefresh>.OnEvent -= HandleEvent;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Bus<CenterTilePanelRefresh>.OnEvent -= HandleEvent;
        }

        public void EnableForUI(CenterTile tile)
        {
            _tile = tile;

            nextTIleBuyUI.EnableForUI(_tileManager.TileBuyPrice);
            currentHasItemPanelUI.EnableForUI(_tile.GetStorage().GetInfoItemValue());
            mHeadQuarterCallButton.EnableForUI();

            Bus<CenterTilePanelRefresh>.OnEvent += HandleEvent;
            gameObject.SetActive(true);
        }

        private void HandleEvent(CenterTilePanelRefresh evt)
        {
            DisableUI();
            EnableForUI(_tile);
        }
    }
}