using IGenerationTool.Utilities;

namespace IGenerationTool.Models
{
    public class Corridor
    {
        public int StartPosX;
        public int StartPosY;
        public int CorridorLength;
        public Direction Direction;

        public int EndPositionX
        {
            get
            {
                if (Direction == Direction.North || Direction == Direction.South)
                    return StartPosX;

                return Direction == Direction.East
                    ? StartPosX + CorridorLength - 1
                    : StartPosX - CorridorLength + 1;
            }
        }

        public int EndPositionY
        {
            get
            {
                if (Direction == Direction.East || Direction == Direction.West)
                    return StartPosY;

                return Direction == Direction.North
                    ? StartPosY + CorridorLength - 1
                    : StartPosY - CorridorLength + 1;
            }
        }
    }
}