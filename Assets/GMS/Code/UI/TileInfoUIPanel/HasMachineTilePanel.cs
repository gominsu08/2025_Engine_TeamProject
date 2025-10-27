using GMS.Code.Core;
using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class HasMachineTilePanel : MonoBehaviour, IUIElement<TileInformation>
    {
        [SerializeField] private CurrentMachineCheck machinePanelUI;
        [SerializeField] private ResourceNameUI resourceNameUI;
        [SerializeField] private ResourceTierUI resourceTierUI;
        [SerializeField] private NextTIleBuyUI nextTierUI;
        [SerializeField] private DescriptionUI descriptionUI;
        [SerializeField] private ResourceCarryingValueUI resourceCarryingValueUI;

        private MachineManager _machineManager;
        private TileInformation _tileInformation;
        private TileManager _tileManager;

        public void Init(TileManager tileManager, MachineManager machineManager)
        {
            _tileManager = tileManager;
            _machineManager = machineManager;
            machinePanelUI.Init();
            resourceCarryingValueUI.Init(machineManager);

        }

        public void EnableForUI(TileInformation tileInfo)
        {
            _tileInformation = tileInfo;

            Refresh();
            gameObject.SetActive(true);
        }

        private void Refresh()
        {
            if (_tileInformation != null && _tileInformation.item != null)
            {
                machinePanelUI.EnableForUI(_tileInformation.item, _machineManager.MachineContainer.GetMachintToTileInfo(_tileInformation).machineSO.machineTier);
                resourceNameUI.EnableForUI(_tileInformation.item.itemName);
                resourceTierUI.EnableForUI(_tileInformation.item.tier);
                descriptionUI.EnableForUI(_machineManager.MachineContainer.GetMachintToTileInfo(_tileInformation).machineSO.machineDescription);
            }
            else
            {
                machinePanelUI.NoneItem();
                resourceNameUI.EnableForUI("ºó¶¥");
                resourceTierUI.EnableForUI(MainPanel.Tier.None);
                descriptionUI.EnableForUI("¾Æ¹«°Íµµ Á¸ÀçÇÏÁö ¾Ê´Â Ã´¹ÚÇÑ ¶¥");
            }

            resourceCarryingValueUI.EnableForUI(_tileInformation);
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