using IGenerationTool.Generation;
using IGenerationTool.Models;
using IGenerationTool.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenerationTool.Generation
{
    public class CorridorBuilder : ICorridorBuilder
    {
        public void BuildCorridor(Corridor corridor, Room room, IntRange corridorLength, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool isFirst = false)
        {
            corridor.Direction = (Direction)Random.Range(0, 4);
            var oppositeDirection = (Direction)(((int)room.EnteringCorridor + 2) % 4);

            if (!isFirst && corridor.Direction == oppositeDirection)
            {
                var directionInt = (int)corridor.Direction;
                directionInt++;
                directionInt = directionInt % 4;
                corridor.Direction = (Direction)directionInt;
            }

            corridor.CorridorLength = corridorLength.Random;
            var maxLength = corridorLength.MaxValue;

            switch (corridor.Direction)
            {
                case Direction.North:
                    corridor.StartPosX = Random.Range(room.XPos, room.XPos + room.RoomWidth - 1);
                    corridor.StartPosY = room.YPos + room.RoomHeight;
                    maxLength = rows - corridor.StartPosY - roomHeight.MinValue;
                    break;
                case Direction.East:
                    corridor.StartPosX = room.XPos + room.RoomWidth;
                    corridor.StartPosY = Random.Range(room.YPos, room.YPos + room.RoomHeight - 1);
                    maxLength = columns - corridor.StartPosX - roomWidth.MinValue;
                    break;
                case Direction.South:
                    corridor.StartPosX = Random.Range(room.XPos, room.XPos + room.RoomWidth);
                    corridor.StartPosY = room.YPos;
                    maxLength = corridor.StartPosY - roomHeight.MinValue;
                    break;
                case Direction.West:
                    corridor.StartPosX = room.XPos;
                    corridor.StartPosY = Random.Range(room.YPos, room.YPos + room.RoomHeight);
                    maxLength = corridor.StartPosX - roomWidth.MinValue;
                    break;
            }

            corridor.CorridorLength = Mathf.Clamp(corridor.CorridorLength, 1, maxLength);
        }
    }
}
