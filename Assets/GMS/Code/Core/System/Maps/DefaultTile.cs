using UnityEngine;
using UnityEngine.EventSystems;

namespace GMS.Code.Core.System.Maps
{
    public class DefaultTile : MonoBehaviour, IPointerClickHandler
    {
        public DefaultTile tilePrefab;
        public GhostTile upTile, downTile, leftTile, rightTile;
        public bool isUpTile, isDownTile, isLeftTile, isRightTile;
        public bool isClick = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClick)
            {
                upTile.gameObject.SetActive(false);
                downTile.gameObject.SetActive(false);
                leftTile.gameObject.SetActive(false);
                rightTile.gameObject.SetActive(false);
            }
            else
            {
                ActiveFalse();
            }

            isClick = !isClick;
        }

        private void ActiveFalse()
        {
            if (!isUpTile)
                upTile.gameObject.SetActive(true);
            if (!isDownTile)
                downTile.gameObject.SetActive(true);
            if (!isLeftTile)
                leftTile.gameObject.SetActive(true);
            if (!isRightTile)
                rightTile.gameObject.SetActive(true);
        }

        public void Awake()
        {
            upTile.OnClick += GhostSet;
            downTile.OnClick += GhostSet;
            leftTile.OnClick += GhostSet;
            rightTile.OnClick += GhostSet;

        }

        public void GhostSet(Direction dir)
        {
            switch (dir)
            {
                case Direction.Right:
                    isRightTile = true;
                    rightTile.gameObject.SetActive(false);
                    DefaultTile tile_right = Instantiate(tilePrefab, rightTile.transform.position, Quaternion.identity);
                    tile_right.isLeftTile = true;
                    break;
                case Direction.Left:
                    isLeftTile = true;
                    leftTile.gameObject.SetActive(false);
                    DefaultTile tile_left = Instantiate(tilePrefab, leftTile.transform.position, Quaternion.identity);
                    tile_left.isRightTile = true;
                    break;
                case Direction.Up:
                    isUpTile = true;
                    upTile.gameObject.SetActive(false);
                    DefaultTile tile_up = Instantiate(tilePrefab, upTile.transform.position, Quaternion.identity);
                    tile_up.isDownTile = true;
                    break;
                case Direction.Down:
                    isDownTile = true;
                    downTile.gameObject.SetActive(false);
                    DefaultTile tile_down = Instantiate(tilePrefab, downTile.transform.position, Quaternion.identity);
                    tile_down.isUpTile = true;
                    break;
                default:
                    break;
            }
        }
    }
}