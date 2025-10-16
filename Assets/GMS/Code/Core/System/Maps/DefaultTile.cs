using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GMS.Code.Core.System.Maps
{
    public class DefaultTile : MonoBehaviour, IClickable
    {
        public DefaultTile tilePrefab;
        public List<GhostTile> ghostTiles = new List<GhostTile>();
        public bool isClick = false;

        private TileInformation _tileInfo;

        public void OnClick()
        {
            if (isClick)
            {
                ClickCancel();
                return;
            }
        }

        public void SetActiveGhost(bool isActive)
        {
            if (isActive)
            {
                if (_tileInfo.isDownTile)
                    GetGhostHasDirection(Direction.Down).Enable();
                if (_tileInfo.isUpTile)
                    GetGhostHasDirection(Direction.Up).Enable();
                if(_tileInfo.isLeftTile)
                    GetGhostHasDirection(Direction.Left).Enable();
                if(_tileInfo.isRightTile)
                    GetGhostHasDirection(Direction.Right).Enable();
            }
            else
            {
                foreach(GhostTile ghostTile in ghostTiles)
                {
                    ghostTile.Disable();
                }
            }
        }

        private void ClickCancel()
        {
            isClick = false;
        }

        public GhostTile GetGhostHasDirection(Direction dir)
        {
            GhostTile result = null;

            foreach(GhostTile ghost in ghostTiles)
            {
                if(ghost.Direction == dir)
                {
                    result = ghost;
                }
            }

            return result;
        }

        
    }
}