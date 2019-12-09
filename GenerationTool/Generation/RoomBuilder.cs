using IGenerationTool.Generation;
using IGenerationTool.Models;
using IGenerationTool.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenerationTool.Generation
{
    public class RoomBuilder : IRoomBuilder
    {
        public void BuildRoom(Room room, IntRange widthRange, IntRange heightRange, int columns, int rows)
        {
            room.RoomWidth = widthRange.Random;
            room.RoomHeight = heightRange.Random;

            room.XPos = Mathf.RoundToInt(columns / 2f - room.RoomWidth / 2f);
            room.YPos = Mathf.RoundToInt(rows / 2f - room.RoomHeight / 2f);
        }

        public void BuildRoom(Room room, IntRange widthRange, IntRange heightRange, int columns, int rows,
            Corridor corridor)
        {
            room.EnteringCorridor = corridor.Direction;

            room.RoomWidth = widthRange.Random;
            room.RoomHeight = heightRange.Random;

            switch (corridor.Direction)
            {
                case Direction.North:
                    room.RoomHeight = Mathf.Clamp(room.RoomHeight, 1, rows - corridor.EndPositionY);

                    room.YPos = corridor.EndPositionY;

                    room.XPos = UnityEngine.Random.Range(corridor.EndPositionX - room.RoomWidth + 1,
                        corridor.EndPositionX);
                    room.XPos = Mathf.Clamp(room.XPos, 0, columns - room.RoomWidth);
                    break;
                case Direction.South:
                    room.RoomHeight = Mathf.Clamp(room.RoomHeight, 1, corridor.EndPositionY);

                    room.YPos = corridor.EndPositionY - room.RoomHeight + 1;

                    room.XPos = UnityEngine.Random.Range(corridor.EndPositionX - room.RoomWidth + 1,
                        corridor.EndPositionX);
                    room.XPos = Mathf.Clamp(room.XPos, 0, columns - room.RoomWidth);
                    break;
                case Direction.East:
                    room.RoomWidth = Mathf.Clamp(room.RoomWidth, 1, columns - corridor.EndPositionX);
                    room.XPos = corridor.EndPositionX;

                    room.YPos = UnityEngine.Random.Range(corridor.EndPositionY - room.RoomHeight + 1,
                        corridor.EndPositionY);
                    room.YPos = Mathf.Clamp(room.YPos, 0, rows - room.RoomHeight);
                    break;
                case Direction.West:
                    room.RoomWidth = Mathf.Clamp(room.RoomWidth, 1, corridor.EndPositionX);
                    room.XPos = corridor.EndPositionX - room.RoomWidth + 1;

                    room.YPos = Random.Range(corridor.EndPositionY - room.RoomHeight + 1, corridor.EndPositionY);
                    room.YPos = Mathf.Clamp(room.YPos, 0, rows - room.RoomHeight);
                    break;
            }
        }
    }
}
