using GMS.Code.Core.Events;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public class DefaultTile : MonoBehaviour, IClickable
    {
        [SerializeField] private DefaultTile tilePrefab;
        [SerializeField] private List<GhostTile> ghostTiles = new List<GhostTile>();
        private bool _isSelect = false;

        public TileInformation TileInfo { get; private set; }

        public void Init(TileInformation myInfo)
        {
            TileInfo = myInfo;
            Bus<TileSelectEvent>.OnEvent += HandleTileSelect;
        }

        private void OnDestroy()
        {
            Bus<TileSelectEvent>.OnEvent -= HandleTileSelect;
        }

        private void HandleTileSelect(TileSelectEvent evt)
        {
            if (TileInfo.x == evt.tileInfo.x && TileInfo.z == evt.tileInfo.z) return;
            SelectCancel();
        }

        public void OnClick()
        {
            if (_isSelect)
            {
                SelectCancel();
                return;
            }

            Bus<TileSelectEvent>.Raise(new TileSelectEvent(TileInfo));
            EnableAllGhost();
        }

        public void EnableAllGhost()
        {
            if (TileInfo.isDownTile)
                GetGhostHasDirection(Direction.Down).Enable(TileInfo);
            if (TileInfo.isUpTile)
                GetGhostHasDirection(Direction.Up).Enable(TileInfo);
            if (TileInfo.isLeftTile)
                GetGhostHasDirection(Direction.Left).Enable(TileInfo);
            if (TileInfo.isRightTile)
                GetGhostHasDirection(Direction.Right).Enable(TileInfo);

            Bus<TileBuyEvent>.OnEvent += HandleBuyTileEvent;
        }

        private void HandleBuyTileEvent(TileBuyEvent evt)
        {
            if (TileInfo.x == evt.x && TileInfo.z == evt.z) return;
            SelectCancel();
            Bus<TileBuyEvent>.OnEvent -= HandleBuyTileEvent;
        }

        public void DisableAllGhost()
        {
            foreach (GhostTile ghostTile in ghostTiles)
            {
                ghostTile.Disable();
            }
        }

        private void SelectCancel()
        {
            _isSelect = false;
            DisableAllGhost();
        }

        public GhostTile GetGhostHasDirection(Direction dir)
        {
            GhostTile result = null;

            foreach (GhostTile ghost in ghostTiles)
            {
                if (ghost.direction == dir)
                {
                    result = ghost;
                }
            }

            return result;
        }


    }
}