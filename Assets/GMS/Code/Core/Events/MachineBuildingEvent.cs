using GMS.Code.Core.System.Machine;
using GMS.Code.Core.System.Maps;
using GMS.Code.UI.MainPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
