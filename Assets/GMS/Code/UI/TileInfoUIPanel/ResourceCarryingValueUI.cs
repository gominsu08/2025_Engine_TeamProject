using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using GMS.Code.UI.MainPanel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMS.Code.UI.TileInfoUIPanel
{
    public class ResourceCarryingValueUI : MonoBehaviour, IUIElement<TileInformation>
    {
        [SerializeField] private TextUI text;

        private TileInformation _tileInfo;
        private  MachineManager _machineManager;

        public void Init(MachineManager machineManager)
        {
            _machineManager = machineManager;
        }

        public void DisableUI()
        {
            _machineManager.MachineContainer.RemoveEventCarryingValueChangedEvent(SetText, _tileInfo);
            text.DisableUI();
            gameObject.SetActive(false);
        }

        public void EnableForUI(TileInformation tileInfo)
        {
            _tileInfo = tileInfo;

            SetText(_machineManager.MachineContainer.AddEventCarryingValueChangedEvent(SetText, tileInfo));
            
            gameObject.SetActive(true);
        }

        public void SetText(int value)
        {
            int cur = value;
            int max = _machineManager.MachineContainer.GetMaxCarrayingValue(_tileInfo);

            text.EnableForUI($"{cur} / {max}");
        }
    }
}
