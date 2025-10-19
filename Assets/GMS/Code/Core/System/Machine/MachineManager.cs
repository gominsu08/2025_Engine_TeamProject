using GMS.Code.Core.Events;
using GMS.Code.Core.System.Maps;
using System;
using UnityEngine;

namespace GMS.Code.Core.System.Machine
{
    public enum MachineType
    {
        None,
        Brazier,
        MiningMachine,
        DrillingMachine,
        LoggingMachine,
        Excavator,
    }

    public class MachineManager : MonoBehaviour
    {
        public void Awake()
        {
            Bus<MachineBuildingEvent>.OnEvent += HandleBuildingMachine;
        }

        public void OnDestroy()
        {
            Bus<MachineBuildingEvent>.OnEvent -= HandleBuildingMachine;
        }

        private void HandleBuildingMachine(MachineBuildingEvent evt)
        {
            //
        }

        public MachineType IsMachineType(TileInformation tileInfo)
        {
            return MachineType.None;
        }

        public float GetMiningTime(TileInformation tileInfo)
        {
            return 5f;
        }

        internal float GetCurrentMiningTime(TileInformation tileInfo)
        {
            return 2.5f;
        }
    }
}
