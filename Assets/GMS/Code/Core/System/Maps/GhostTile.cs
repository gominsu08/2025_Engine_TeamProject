using DG.Tweening;
using GMS.Code.Core.Events;
using PSW.Code.Container;
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

        [SerializeField] private Color tileBuyFailColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private MeshRenderer ghostTileRenderer;

        private int _x, _z;
        private Material _material_1;
        private Material _material_2;
        private ResourceContainer _resourceContainer;
        private TileManager _tileManager;
        private TileInformation _parentInfo;


        public void Init(ResourceContainer container, TileManager tileManager, TileInformation info)
        {
            _material_1 = ghostTileRenderer.materials[0];
            _material_2 = ghostTileRenderer.materials[1];
            _resourceContainer = container;
            _tileManager = tileManager;
            _parentInfo = info;

            if (_parentInfo == null) return;

            switch (direction)
            {
                case Direction.Right:
                    _x = _parentInfo.x + 1;
                    _z = _parentInfo.z;
                    break;
                case Direction.Left:
                    _x = _parentInfo.x - 1;
                    _z = _parentInfo.z;
                    break;
                case Direction.Up:
                    _x = _parentInfo.x;
                    _z = _parentInfo.z + 1;
                    break;
                case Direction.Down:
                    _x = _parentInfo.x;
                    _z = _parentInfo.z - 1;
                    break;
            }
        }

        public void Enable()
        {
            _material_1.SetColor("_Color", defaultColor);
            _material_2.SetColor("_Color", defaultColor);
            gameObject.SetActive(true);
        }

        public void OnClick()
        {
            if (_resourceContainer.IsTargetCoin(_tileManager.TileBuyPrice))
            {
                _resourceContainer.MinusCoin(_tileManager.TileBuyPrice);
                Bus<TileBuyEvent>.Raise(new TileBuyEvent { x = _x, z = _z, direction = direction });
                gameObject.SetActive(false);
            }
            else
            {
                _material_1.SetColor("_Color", tileBuyFailColor);
                _material_2.SetColor("_Color", tileBuyFailColor);
                DOVirtual.DelayedCall(0.25f, () =>
                {
                    _material_1.SetColor("_Color", defaultColor);
                    _material_2.SetColor("_Color", defaultColor);
                });
                return;
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
