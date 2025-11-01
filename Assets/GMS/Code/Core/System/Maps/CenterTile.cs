using PSW.Code.Container;
using System;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public class CenterTile : DefaultTile
    {
        [SerializeField] private Storage storage;

        public override void Init(TileInformation myInfo, ResourceContainer resourceContainer, TileManager tileManager, bool isBuy = false)
        {
            base.Init(myInfo, resourceContainer, tileManager, isBuy);
            storage.Init();
        }

        public Storage GetStorage() { return storage; }
    }
}