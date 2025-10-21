using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using GMS.Code.UI.MainPanel;

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
