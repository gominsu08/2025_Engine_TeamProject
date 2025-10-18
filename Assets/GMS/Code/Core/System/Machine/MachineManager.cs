using GMS.Code.Core.System.Maps;
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

        public MachineType IsMachineType(TileInformation tileInfo)
        {
            return MachineType.MiningMachine;
        }

        public int GetMiningTime(TileInformation tileInfo)
        {
            return 1;
        }
    }
}
