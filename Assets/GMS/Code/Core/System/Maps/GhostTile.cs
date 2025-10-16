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

    public class GhostTile : MonoBehaviour, IClickable
    {
        public Action<Direction> OnClickEvent;
        public Direction Direction;

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void OnClick()
        {
            OnClickEvent?.Invoke(Direction);
            gameObject.SetActive(false);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
