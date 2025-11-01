using GMS.Code.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Code.Core.Events
{
    public struct StorageItemAddEvent : IEvent
    {
        public ItemSO Item { get; private set; }
        public int Value { get; private set; }

        public StorageItemAddEvent(ItemSO item, int value)
        {
            Item = item;
            Value = value;
        }
    }

    public struct CenterTilePanelRefresh : IEvent { }
}
