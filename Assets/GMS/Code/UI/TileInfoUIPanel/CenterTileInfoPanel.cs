using GMS.Code.Core.System.Maps;
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
            gameObject.SetActive(false);
        }

        public void EnableForUI(CenterTile tile)
        {
            _tile = tile;

            nextTIleBuyUI.EnableForUI(_tileManager.TileBuyPrice);
            currentHasItemPanelUI.EnableForUI(_tile.GetStorage().GetInfoItemValue());
            mHeadQuarterCallButton.EnableForUI();
            gameObject.SetActive(true);
        }
    }
}