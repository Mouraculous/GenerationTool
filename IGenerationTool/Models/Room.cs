using IGenerationTool.Utilities;

namespace IGenerationTool.Models
{
    public class Room
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int RoomWidth { get; set; }
        public int RoomHeight { get; set; }
        public Direction EnteringCorridor { get; set; } = Direction.North;
    }
}
