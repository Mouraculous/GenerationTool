using IGenerationTool.Models;
using IGenerationTool.Utilities;
using System.Collections.Generic;

namespace IGenerationTool.Generation
{
    public interface ITileLayoutCreator
    {
        void SetupRoomTiles(IEnumerable<Room> rooms, ref TileType[][] tiles);
        void SetupCorridorTiles(IEnumerable<Corridor> corridors, ref TileType[][] tiles);
    }
}
