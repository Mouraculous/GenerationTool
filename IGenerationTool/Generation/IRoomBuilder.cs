using IGenerationTool.Models;
using IGenerationTool.Utilities;

namespace IGenerationTool.Generation
{
    public interface IRoomBuilder
    {
        void BuildRoom(Room room, IntRange widthRange, IntRange heightRange, int columns, int rows);
        void BuildRoom(Room room, IntRange widthRange, IntRange heightRange, int columns, int rows,
            Corridor corridor);
    }
}
