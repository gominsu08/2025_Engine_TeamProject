using GMS.Code.Core.Events;
using GMS.Code.Items;
using PSW.Code.Container;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEditor.Purchasing;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public struct EndAddTileEvent : IEvent
    {

    }

    public class TileInformation
    {
        public int x, z;
        public bool isUpTile, isDownTile, isLeftTile, isRightTile;
        public DefaultTile tileObject;
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

    public class TileContainer
    {
        private int _startTileCreateCount = 0;
        private CenterTile _centerTile;
        private DefaultTile _tile;
        private DefaultTile _treeTile;
        private List<DefaultTile> _startTile = new List<DefaultTile>();
        private List<DefaultTile> _tierOneTiles = new List<DefaultTile>();
        private List<DefaultTile> _tierTwoTiles = new List<DefaultTile>();
        private List<DefaultTile> _tierThreeTiles = new List<DefaultTile>();

        public DefaultTile GetRandomTile(int curTileCount)
        {
            if (curTileCount < 25)
                return _tierOneTiles[Random.Range(0, _tierOneTiles.Count)];
            else if (curTileCount < 65)
            {
                DefaultTile _1tile = _tierOneTiles[Random.Range(0, _tierOneTiles.Count)];
                DefaultTile _2tile = _tierTwoTiles[Random.Range(0, _tierTwoTiles.Count)];

                if (Random.value > 0.5f)
                    return _1tile;
                else
                    return _2tile;
            }
            else
            {
                DefaultTile _1tile = _tierOneTiles[Random.Range(0, _tierOneTiles.Count)];
                DefaultTile _2tile = _tierTwoTiles[Random.Range(0, _tierTwoTiles.Count)];
                DefaultTile _3tile = _tierThreeTiles[Random.Range(0, _tierThreeTiles.Count)];

                DefaultTile selectTile;
                float randomValue = Random.value;
                if (randomValue < 0.2f)
                {
                    selectTile = _1tile;
                }
                else if (randomValue < (curTileCount > 100 ? 0.7f : 0.5f))
                {
                    selectTile = _2tile;
                }
                else
                {
                    selectTile = _3tile;
                }

                return selectTile;
            }
        }

        public DefaultTile GetStartTile()
        {
            _startTileCreateCount++;
            return _startTile[_startTileCreateCount - 1];
        }

        public void StartTileSet()
        {
            _startTile.Add(_centerTile);
            _startTile.Add(_tile);
            _startTile.Add(_treeTile);
            _startTile.Add(_tile);
            _startTile.Add(_tile);
            _startTile.Add(_treeTile);
            _startTile.Add(_tile);
            _startTile.Add(_tile);
            _startTile.Add(_treeTile);
        }

        public TileContainer(List<DefaultTile> tilePrefabs)
        {
            foreach (DefaultTile tile in tilePrefabs)
            {
                if (tile is CenterTile center)
                {
                    _centerTile = center;
                    continue;
                }
                else if (!(tile is ResourceTile resourceTile))
                {
                    _tile = tile;
                    _tierOneTiles.Add(tile);
                    continue;
                }



                else if (resourceTile.GetResourceItem().tier == UI.MainPanel.Tier.FirstTier)
                {
                    _tierOneTiles.Add(tile);
                    if (resourceTile.GetResourceItem().itemType == ItemType.Tree)
                        _treeTile = tile;
                }
                else if (resourceTile.GetResourceItem().tier == UI.MainPanel.Tier.SecondTier)
                {
                    _tierTwoTiles.Add(tile);
                }
                else if (resourceTile.GetResourceItem().tier == UI.MainPanel.Tier.ThirdTier)
                {
                    _tierThreeTiles.Add(tile);
                }
            }

            StartTileSet();
        }
    }

    public struct NavMeshSurfaceBaceEvent : IEvent { }

    public class TileManager : MonoBehaviour
    {
        public int TileBuyPrice => 1000 + ((_tileCount - initialTileCount) * 200);

        [SerializeField] private NavMeshSurface _surface;
        [SerializeField] private List<DefaultTile> TilePrefabs;
        [SerializeField] private ResourceContainer resourceContainer;
        [SerializeField] private int initialTileCount = 9;
        private List<TileInformation> tiles = new List<TileInformation>();
        private TileContainer _tileContainer;
        private int _tileCount = 0;

        private void Awake()
        {
            _tileContainer = new TileContainer(TilePrefabs);
            StartTileSet();
            Bus<TileBuyEvent>.OnEvent += HandleBuyTileEvent;
            Bus<NavMeshSurfaceBaceEvent>.OnEvent += HandleTileSelect;
        }

        private void OnDestroy()
        {
            Bus<TileBuyEvent>.OnEvent -= HandleBuyTileEvent;
            Bus<NavMeshSurfaceBaceEvent>.OnEvent -= HandleTileSelect;
        }

        private void HandleTileSelect(NavMeshSurfaceBaceEvent evt)
        {
            _surface.BuildNavMesh();
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
            Bus<EndAddTileEvent>.Raise(new EndAddTileEvent());
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
            DefaultTile tile = Instantiate(GetTilePrefab(isStartTileSet), new Vector3(tileInfo.x, 0, tileInfo.z), Quaternion.identity);
            if (tile is ResourceTile resourceTile)
                tileInfo.item = resourceTile.GetResourceItem();
            tile.name = $"Tile {tileInfo.x} {tileInfo.z}";
            tile.transform.parent = transform;
            tileInfo.tileObject = tile;
            tile.Init(tileInfo, resourceContainer, this, !isStartTileSet);
            _tileCount++;
            _surface.BuildNavMesh();
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

            for (int i = 0; i < 9; i++)
            {
                TileInformation result = CreateTileInfo(startTilePos[i, 0], startTilePos[i, 1]);
                CreateTile(result, true);
                tiles.Add(result);
            }


        }

        private DefaultTile GetTilePrefab(bool isStartTileSet)
        {
            if (isStartTileSet == false)
                return _tileContainer.GetRandomTile(_tileCount);
            else
                return _tileContainer.GetStartTile();
        }

        public TileInformation GetRandomTile() => tiles[Random.Range(0, tiles.Count)];

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

        public List<TileInformation> GetTileToItem(ItemSO currentTargetItem)
        {
            List<TileInformation> result = new List<TileInformation>();
            result = tiles.Aggregate(new List<TileInformation>(),(list , item) =>
            {
                if(item.item != null && item.item.itemName == currentTargetItem.itemName)
                    list.Add(item);
                return list;
            });

            return result;
        }
    }
}