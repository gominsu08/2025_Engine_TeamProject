using MK.Toon;
using System.Collections.Generic;
using UnityEngine;

namespace GMS.Code.Core.System.Maps
{
    public class TileInformation
    {
        public int x, z;
        public bool isUpTile, isDownTile, isLeftTile, isRightTile;

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
        public List<TileInformation> tiles = new List<TileInformation>();
        [SerializeField] private DefaultTile TilePrefab;

        public void AddTile(int x, int z)
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

                if(left.x == tile.x && left.y == tile.z)
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
            TileInformation result = new TileInformation(x,z);
            result.isRightTile = rightTile;
            result.isLeftTile = leftTile;
            result.isUpTile = upTile;
            result.isDownTile = downTile;
            tiles.Add(result);

            
        }

        public TileInformation GetTileInfo(int x , int z)
        {
            TileInformation result = new TileInformation(0,0);

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