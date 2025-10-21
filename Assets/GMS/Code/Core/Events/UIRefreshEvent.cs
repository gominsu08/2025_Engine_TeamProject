using GMS.Code.Core.System.Machines;
using GMS.Code.Core.System.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Code.Core.Events
{
    public struct UIRefreshEvent : IEvent
    {
        public TileInformation info;

        public UIRefreshEvent(TileInformation targetInfo)
        {
            info = targetInfo;
        }
    }
}
