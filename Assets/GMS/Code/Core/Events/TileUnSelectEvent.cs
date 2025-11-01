using GMS.Code.Core.System.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Code.Core.Events
{
    public struct TileUnSelectEvent : IEvent
    {
        public TileInformation tileInfo;
        public bool isBuy;

        public TileUnSelectEvent(TileInformation tile, bool isBuy = false)
        {
            tileInfo = tile;
            this.isBuy = isBuy;
        }
    }

    public struct TileUseUnSelectEvent : IEvent
    {
        public TileInformation tileInfo;

        public TileUseUnSelectEvent(TileInformation tile)
        {
            tileInfo = tile;
        }
    }
}
