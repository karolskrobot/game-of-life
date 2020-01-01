using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core
{
    public class Board : IBoard
    {
        public Board(bool[,] boardArray)
        {
            _boardArray = boardArray;
        }

        private bool[,] _boardArray;

        public int LengthRows => _boardArray.GetLength(0);

        public int LengthColumns => _boardArray.GetLength(1);

        public bool GetTileValue(int row, int column) => _boardArray[row, column];

        public void Evolve()
        {
            var evolvedBoardArray = new bool[LengthRows, LengthColumns];

            for (var row = 0; row < LengthRows; row++)
            {
                for (var col = 0; col < LengthColumns; col++)
                {
                    IEnumerable<bool> neighbours = GetTileNeighbours(row, col);

                    int aliveNeighboursCount = GetAliveNeighboursCount(neighbours);

                    bool centerTileValue = GetTileValue(row, col);

                    if (centerTileValue == false && aliveNeighboursCount == 3)
                    {
                        evolvedBoardArray[row, col] = true;
                    }
                    else if (centerTileValue == true && (aliveNeighboursCount < 2 || aliveNeighboursCount > 3))
                    {
                        evolvedBoardArray[row, col] = false;
                    }
                    else
                    {
                        evolvedBoardArray[row, col] = centerTileValue;
                    }
                }
            }

            _boardArray = evolvedBoardArray;
        }

        private IEnumerable<bool> GetTileNeighbours(int centerTileRow, int centerTileColumn)
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

        private int GetAliveNeighboursCount(IEnumerable<bool> neighbours) => neighbours.Count(n => n == true);
    }
}
