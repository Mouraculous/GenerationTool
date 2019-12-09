using IGenerationTool.Models;
using IGenerationTool.Utilities;

namespace IGenerationTool.Generation
{
    public interface ICorridorBuilder
    {
        void BuildCorridor(Corridor corridor, Room room, IntRange corridorLength, IntRange roomWidth,
            IntRange roomHeight, int columns, int rows, bool isFirst = false);
    }
}
