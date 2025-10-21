using GMS.Code.Core.Events;
using GMS.Code.Items;
using PSW.Code.Container;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public class TileInformation
    {
        public int x, z;
        public bool isUpTile, isDownTile, isLeftTile, isRightTile;
        public GameObject tileObject;
        public ItemSO item;

        public List<Direction> GetAdjacentTiles()
        {
            List<Direction> result = new List<Direction>();

            if (isUpTile)
                result.Add(Direction.Up);
            if (isDownTile)
                result.Add(Direction.Down);
            if (isLeftTile)
                result.Add(Direction.Left);
            if (isRightTile)
                result.Add(Direction.Right);

            return result;
        }

        public void IsActiveTileInfo(Vector2 dir)
        {
            if (dir.x == 1 && dir.y == 0)
                isRightTile = true;
            if (dir.x == 1 && dir.y == 0)
                isRightTile = true;
            if (dir.x == 1 && dir.y == 0)
                isRightTile = true;
            if (dir.x == 1 && dir.y == 0)
                isRightTile = true;
        }

        public TileInformation(int x, int z)
        {
            this.x = x;
            this.z = z;
            isUpTile = false;
            isDownTile = false;
            isLeftTile = false;
            isRightTile = false;
        }
    }

    public class TileManager : MonoBehaviour
    {
        public int TileBuyPrice => 1000 + ((_tileCount - initialTileCount) * 200);

        [SerializeField] private List<DefaultTile> TilePrefabs;
        [SerializeField] private ResourceContainer resourceContainer;
        [SerializeField] private int initialTileCount = 9;
        private List<TileInformation> tiles = new List<TileInformation>();
        private int _tileCount = 0;

        private void Awake()
        {
            StartTileSet();
            Bus<TileBuyEvent>.OnEvent += HandleBuyTileEvent;
        }

        private void HandleBuyTileEvent(TileBuyEvent evt)
        {
            AddTile(evt.x, evt.z);
        }

        public void AddTile(int x, int z)
        {
            TileInformation result = CreateTileInfo(x, z);
            CreateTile(result);
            tiles.Add(result);
        }

        private TileInformation CreateTileInfo(int x, int z)
        {
            Vector2 right = new Vector2(x + 1, z);
            Vector2 left = new Vector2(x - 1, z);
            Vector2 up = new Vector2(x, z + 1);
            Vector2 down = new Vector2(x, z - 1);

            bool upTile = false, downTile = false, leftTile = false, rightTile = false;

            foreach (TileInformation tile in tiles)
            {
                if (right.x == tile.x && right.y == tile.z)
                {
                    rightTile = true;
                    tile.isLeftTile = true;
                }

                if (left.x == tile.x && left.y == tile.z)
                {
                    leftTile = true;
                    tile.isRightTile = true;
                }

                if (up.x == tile.x && up.y == tile.z)
                {
                    upTile = true;
                    tile.isDownTile = true;
                }

                if (down.x == tile.x && down.y == tile.z)
                {
                    downTile = true;
                    tile.isUpTile = true;
                }
            }
            TileInformation result = new TileInformation(x, z);
            result.isRightTile = rightTile;
            result.isLeftTile = leftTile;
            result.isUpTile = upTile;
            result.isDownTile = downTile;
            return result;
        }

        private void CreateTile(TileInformation tileInfo, bool isStartTileSet = false)
        {
            DefaultTile tile = Instantiate(GetTilePrefab(), new Vector3(tileInfo.x, 0, tileInfo.z), Quaternion.identity);
            if (tile is ResourceTile resourceTile)
                tileInfo.item = resourceTile.GetResourceItem();
            tile.name = $"Tile {tileInfo.x} {tileInfo.z}";
            tile.transform.parent = transform;
            tileInfo.tileObject = tile.gameObject;
            tile.Init(tileInfo, resourceContainer, this, !isStartTileSet);
            _tileCount++;
        }

        public void StartTileSet()
        {
            int[,] startTilePos = new int[,]
            {
                {0, 0   },
                {0, 1   },
                {1, 0   },
                {-1, 0  },
                {0, -1  },
                {-1, -1 },
                {-1, 1  },
                {1, -1  },
                {1, 1   },
            };
            
            for(int i = 0; i < 9; i++)
            {
                TileInformation result = CreateTileInfo(startTilePos[i,0], startTilePos[i, 1]);
                CreateTile(result, true);
                tiles.Add(result);
            }
        }

        private DefaultTile GetTilePrefab()
        {
            return TilePrefabs[UnityEngine.Random.Range(0, TilePrefabs.Count)];
        }

        public TileInformation GetTileInfo(int x, int z)
        {
            TileInformation result = new TileInformation(0, 0);

            foreach (TileInformation tile in tiles)
            {
                if (tile.x == x && tile.z == z)
                {
                    result = tile;
                }
            }

            return result;
        }
    }
}