using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GMS.Code.Core.System.Maps
{
    public enum Direction
    {
        Right,
        Left, Up, Down
    }

    public class GhostTile : MonoBehaviour, IPointerClickHandler
    {
        public Action<Direction> OnClick;
        public Direction Direction;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(Direction);
        }
    }
}
