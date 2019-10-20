using System.Collections.Generic;

namespace GameOfLife
{
    public interface IBoard
    {
        bool[,] BoardArray { get; set; }

        int LengthRows { get; }

        int LengthColumns { get; }

        bool GetTileValue(int row, int column);

        void SetTileValue(int row, int column, bool value);

        IEnumerable<bool> GetTileNeighbours(int centerTileRow, int centerTileColumn);
    }
}