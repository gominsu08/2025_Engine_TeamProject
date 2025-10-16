using GMS.Code.Core.System.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Code.Core.Events
{
    public struct TileBuyEvent : IEvent
    {
        public int x, z;
        public Direction direction;


        public TileBuyEvent(int x, int z, Direction dir)
        {
            this.x = x;
            this.z = z;
            direction = dir;
        }
    } 
}
