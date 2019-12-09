using IGenerationTool.Models;
using IGenerationTool.Utilities;
using System.Collections.Generic;
using IGenerationTool.Generation;

namespace GenerationTool.Generation
{
    public class TileLayoutCreator : ITileLayoutCreator
    {
        public void SetupRoomTiles(IEnumerable<Room> rooms, ref TileType[][] tiles)
        {
            foreach (var currentRoom in rooms)
            {
                for (var i = 0; i < currentRoom.RoomWidth; i++)
                {
                    var xCoord = currentRoom.XPos + i;

                    for (var j = 0; j < currentRoom.RoomHeight; j++)
                    {
                        var yCoord = currentRoom.YPos + j;

                        tiles[xCoord][yCoord] = TileType.Floor;
                    }
                }
            }
        }

        public void SetupCorridorTiles(IEnumerable<Corridor> corridors, ref TileType[][] tiles)
        {
            foreach (var currentCorridor in corridors)
            {
                for (var i = 0; i < currentCorridor.CorridorLength; i++)
                {
                    var xCoord = currentCorridor.StartPosX;
                    var yCoord = currentCorridor.StartPosY;

                    switch (currentCorridor.Direction)
                    {
                        case Direction.North:
                            yCoord += i;
                            break;
                        case Direction.South:
                            yCoord -= i;
                            break;
                        case Direction.East:
                            xCoord += i;
                            break;
                        case Direction.West:
                            xCoord -= i;
                            break;
                    }

                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }
}
