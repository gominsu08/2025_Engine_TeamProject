using GMS.Code.Core.System.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Code.Utill
{
    public static class TileUtill
    {
        public static bool IsSame(TileInformation tile_1 , TileInformation tile_2)
        {
            if (tile_1.x == tile_2.x && tile_1.z == tile_2.z)
            {
                return true;
            }
            return false;
        }
    }
}
