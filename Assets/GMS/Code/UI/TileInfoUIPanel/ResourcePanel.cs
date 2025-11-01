using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Maps;
using System;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class ResourcePanel : MonoBehaviour, IUIElement<TileInformation>
    {
        [SerializeField] private MachinePanelUI machinePanelUI;
        [SerializeField] private ResourceNameUI resourceNameUI;
        [SerializeField] private ResourceTierUI resourceTierUI;
        [SerializeField] private NextTIleBuyUI nextTierUI;
        [SerializeField] private DescriptionUI descriptionUI;

        private TileInformation _tileInformation;
        private TileManager _tileManager;

        public void Init(TileManager tileManager)
        {
            _tileManager = tileManager;
            machinePanelUI.Init();

            Bus<EndAddTileEvent>.OnEvent += HandleTileBuyEvent;
        }

        private void OnDestroy()
        {
            Bus<EndAddTileEvent>.OnEvent -= HandleTileBuyEvent;
        }

        private void HandleTileBuyEvent(EndAddTileEvent evt)
        {
            Refresh();
        }

        public void EnableForUI(TileInformation tileInfo)
        {
            _tileInformation = tileInfo;

            Refresh();
            gameObject.SetActive(true);
        }

        private void Refresh()
        {
            if (_tileInformation.item != null)
            {
                machinePanelUI.EnableForUI(_tileInformation.item);
                resourceNameUI.EnableForUI(_tileInformation.item.itemName);
                resourceTierUI.EnableForUI(_tileInformation.item.tier);
                descriptionUI.EnableForUI(_tileInformation.item.itemDescription);
            }
            else
            {
                machinePanelUI.NoneItem();
                resourceNameUI.EnableForUI("ºó¶¥");
                resourceTierUI.EnableForUI(MainPanel.Tier.None);
                descriptionUI.EnableForUI("¾Æ¹«°Íµµ Á¸ÀçÇÏÁö ¾Ê´Â Ã´¹ÚÇÑ ¶¥");
            }

            nextTierUI.EnableForUI(_tileManager.TileBuyPrice);
        }

        public void DisableUI()
        {
            machinePanelUI.DisableUI();
            resourceNameUI.DisableUI();
            resourceTierUI.DisableUI();
            descriptionUI.DisableUI();
            nextTierUI.DisableUI();
            gameObject.SetActive(false);
        }

        
    }
}