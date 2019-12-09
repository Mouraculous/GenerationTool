using IGenerationTool.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerationTool.Utilities
{
    public class ProximityChecker : IProximityChecker
    {
        public bool CheckRoomProximity(int xCoord, int yCoord, TileType[][] tiles)
        {
            var tile = tiles[xCoord][yCoord];

            if (tile == TileType.Floor)
            {
                return false;
            }

            if (yCoord < tiles[xCoord].Length - 1 && tiles[xCoord][yCoord + 1] == TileType.Floor)
            {
                return true;
            }

            if (yCoord != 0 && tiles[xCoord][yCoord - 1] == TileType.Floor)
            {
                return true;
            }

            if (xCoord < tiles.Length - 1 && tiles[xCoord + 1][yCoord] == TileType.Floor)
            {
                return true;
            }

            return xCoord != 0 && tiles[xCoord - 1][yCoord] == TileType.Floor;
        }
    }
}
