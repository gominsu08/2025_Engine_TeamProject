using DG.Tweening;
using GMS.Code.Core;
using GMS.Code.Core.Events;
using GMS.Code.Core.System.Maps;
using System;
using UnityEngine;

namespace GMS.Code.HeadQuarters
{
    public class HeadQuarter : MonoBehaviour
    {
        [SerializeField] private TileManager tileManager;
        [SerializeField] private HeadQuarterMovement quarterMovement;
        private int moveCount;

        public void Awake()
        {
            Bus<HeadQurterCallEvent>.OnEvent += HandleCallEvent;
        }

        private void OnDestroy()
        {
            Bus<HeadQurterCallEvent>.OnEvent -= HandleCallEvent;
            
        }

        private void HandleCallEvent(HeadQurterCallEvent evt)
        {
            if (quarterMovement.IsCenter) return;
            moveCount = 0;
            quarterMovement.MoveStart(tileManager.GetTileInfo(0, 0), true);
        }

        public void Start()
        {
            Move();
        }

        [ContextMenu("Move")]
        public void Move()
        {
            moveCount++;
            if (moveCount == 3)
            {
                moveCount = 0;
                quarterMovement.MoveStart(tileManager.GetTileInfo(0,0),true);
                return;
            }
            quarterMovement.MoveStart(tileManager.GetRandomTile());
        }

        public void MoveEnd()
        {
            Move();
        }
    }
}