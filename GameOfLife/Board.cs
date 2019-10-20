using System.Collections.Generic;

namespace GameOfLife
{
    public class Board : IBoard
    {
        public bool[,] BoardArray { get; set; }

        public int LengthRows => BoardArray.GetLength(0);

        public int LengthColumns => BoardArray.GetLength(1);

        public bool GetTileValue(int row, int column) => BoardArray[row, column];

        public void SetTileValue(int row, int column, bool value) => BoardArray[row, column] = value;

        public IEnumerable<bool> GetTileNeighbours(int centerTileRow, int centerTileColumn)
        {
            var neighbours = new List<bool>();

            // set coords to form 3x3 area around the center tile
            var rowPreceding = centerTileRow - 1;
            var rowFollowing = centerTileRow + 1;
            var columnPreceding = centerTileColumn - 1;
            var columnFollowing = centerTileColumn + 1;

            for (var row = rowPreceding; row <= rowFollowing; row++)
            {
                for (var col = columnPreceding; col <= columnFollowing; col++)
                {
                    if (row == centerTileRow && col == centerTileColumn)
                    {
                        continue; //ignore the center tile
                    }

                    if (row >= LengthRows || row < 0 || col >= LengthColumns || col < 0)
                    {
                        neighbours.Add(false); //if neighbour out of board bounds, add as dead
                    }
                    else
                    {
                        neighbours.Add(GetTileValue(row, col));
                    }
                }
            }

            return neighbours;
        }
    }
}
