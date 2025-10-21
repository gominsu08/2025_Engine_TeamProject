using DG.Tweening;
using GMS.Code.Core.Events;
using GMS.Code.Utill;
using PSW.Code.Container;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public class DefaultTile : MonoBehaviour, IClickable
    {
        public TileInformation TileInfo { get; private set; }
        
        private bool _isSelect = false;
        private TileManager _tileManager;
        private ResourceContainer _resourceContainer;
        private float _startYPos;

        [SerializeField] private TileAnimator animator;
        [SerializeField] private List<GhostTile> ghostTiles = new List<GhostTile>();

        public virtual void Init(TileInformation myInfo, ResourceContainer resourceContainer, TileManager tileManager, bool isBuy = false)
        {
            TileInfo = myInfo;
            _resourceContainer = resourceContainer;
            _tileManager = tileManager;

            Bus<TileSelectEvent>.OnEvent += HandleTileSelect;

            foreach (GhostTile ghost in ghostTiles)
            {
                ghost.Init(_resourceContainer,_tileManager,TileInfo);
            }
            _startYPos = transform.position.y;

            if(isBuy)
                OnClick();
        }

        public virtual void OnDestroy()
        {
            Bus<TileSelectEvent>.OnEvent -= HandleTileSelect;
        }

        private void HandleTileSelect(TileSelectEvent evt)
        {
            if (TileUtill.IsSame(TileInfo,evt.tileInfo) || !_isSelect) return;
            UnSelect();
        }

        public virtual void OnClick()
        {
            if (_isSelect)
            {
                UnSelect();
                return;
            }

            _isSelect = true;
            Bus<TileSelectEvent>.Raise(new TileSelectEvent(TileInfo));
            EnableAllGhost();
            TileSelect();
        }

        private void TileSelect()
        {
            animator.SetMaterial(true);
            transform.DOMoveY(_startYPos + 0.5f,0.2f);
        }

        public virtual void UnSelect()
        {
            _isSelect = false;
            Bus<TileUnSelectEvent>.Raise(new TileUnSelectEvent(TileInfo));
            animator.SetMaterial(false);
            transform.DOMoveY(_startYPos, 0.2f);
            DisableAllGhost();
        }

        public void EnableAllGhost()
        {
            if (!TileInfo.isDownTile)
                GetGhostHasDirection(Direction.Down).Enable();
            if (!TileInfo.isUpTile)
                GetGhostHasDirection(Direction.Up).Enable();
            if (!TileInfo.isLeftTile)
                GetGhostHasDirection(Direction.Left).Enable();
            if (!TileInfo.isRightTile)
                GetGhostHasDirection(Direction.Right).Enable();

            Bus<TileBuyEvent>.OnEvent += HandleBuyTileEvent;
        }

        private void HandleBuyTileEvent(TileBuyEvent evt)
        {
            if (TileInfo.x == evt.x && TileInfo.z == evt.z) return;
            _isSelect = false;
            Bus<TileUnSelectEvent>.Raise(new TileUnSelectEvent(TileInfo, true));
            animator.SetMaterial(false);
            transform.DOMoveY(_startYPos, 0.2f);
            DisableAllGhost();
            Bus<TileBuyEvent>.OnEvent -= HandleBuyTileEvent;
        }

        public virtual void DisableAllGhost()
        {
            foreach (GhostTile ghostTile in ghostTiles)
            {
                ghostTile.Disable();
            }
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