using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using GMS.Code.UI.MainPanel;
using NUnit.Framework;
using System.Collections.Generic;

namespace GMS.Code.Core.Events
{
    public struct MachineBuildingEvent : IEvent
    {
        public Tier tier;
        public MachineType machineType;
        public TileInformation tileInformation;

        public MachineBuildingEvent(Tier targetTier, MachineType targetMachineType, TileInformation targetTileInfo)
        {
            tier = targetTier;
            machineType = targetMachineType;
            tileInformation = targetTileInfo;
        }
    }

    public struct MachineBuildingFailEvent : IEvent
    {
        public Tier machineTier;
        public List<bool> trues;
        public MachineType machineType;
        public MachineBuildingFailEvent(Tier t, MachineType tileInfo, List<bool> list)
        {
            trues = list;
            machineTier = t;
            machineType = tileInfo;
        }
    }
}
