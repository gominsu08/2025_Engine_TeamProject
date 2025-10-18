using GMS.Code.Core.System.Machine;
using GMS.Code.Core.System.Maps;
using GMS.Code.Items;
using PSW.Code.Container;
using UnityEngine;

namespace GMS.Code.UI.MainPanel
{
    public class ResourceMiningPanel : MonoBehaviour, IUIElement<ItemSO, TileInformation>
    {
        [SerializeField] private BarUI barUI;
        private bool _isEnabled = false;
        private TileInformation _tileInfo;
        private MachineManager _machineManager;

        public void Init(MachineManager machineManager)
        {
            _machineManager = machineManager;
        }

        public void EnableForUI(ItemSO targetItem, TileInformation tileInfo)
        {
            _tileInfo = tileInfo;
            barUI.EnableForUI(_machineManager.GetMiningTime(_tileInfo));
            gameObject.SetActive(true);
            _isEnabled = true;
        }

        public void Update()
        {
            if (_isEnabled)
                barUI.UpdateUI(_machineManager.GetMiningTime(_tileInfo));
        }

        public void DisableUI()
        {
            barUI.DisableUI();
            gameObject.SetActive(false);
            _isEnabled = false;
        }
    }
}
