using GMS.Code.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public class ResourceTile : DefaultTile
    {
        [SerializeField] private ItemSO resourceItem;
        internal ItemSO GetResourceItem() => resourceItem;
    }
}
