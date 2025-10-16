using GMS.Code.Core.System.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Code.Core.Events
{
    public struct TileSelectEvent : IEvent
    {
        public TileInformation tileInfo;

        public TileSelectEvent(TileInformation tileInfo)
        {
            this.tileInfo = tileInfo;
        }
    }
}
