namespace IGenerationTool.Utilities
{
    public interface IProximityChecker
    {
        bool CheckRoomProximity(int xCoord, int yCoord, TileType[][] tiles);
    }
}
