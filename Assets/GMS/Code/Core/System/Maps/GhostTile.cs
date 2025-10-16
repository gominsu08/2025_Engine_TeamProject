using GMS.Code.Core.Events;
using System;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public enum Direction
    {
        Right,
        Left, Up, Down
    }

    public class GhostTile : MonoBehaviour, IClickable
    {
        public Direction direction;
        private int _x, _z;

        public void Enable(TileInformation parentInfo)
        {
            if (parentInfo == null) return;

            switch (direction)
            {
                case Direction.Right:
                    _x = parentInfo.x + 1;
                    _z = parentInfo.z;
                    break;
                case Direction.Left:
                    _x = parentInfo.x - 1;
                    _z = parentInfo.z;
                    break;
                case Direction.Up:
                    _x = parentInfo.x;
                    _z = parentInfo.z + 1;
                    break;
                case Direction.Down:
                    _x = parentInfo.x;
                    _z = parentInfo.z - 1;
                    break;
            }
            
            gameObject.SetActive(true);
        }

        public void OnClick()
        {
            Bus<TileBuyEvent>.Raise(new TileBuyEvent { x = _x, z = _z , direction = direction });
            gameObject.SetActive(false);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
