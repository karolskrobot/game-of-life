using System;

namespace GameOfLife.BoardArrayStrategies
{
    public class BoardArrayStrategyRandom : IBoardArrayStrategy
    {
        private readonly int _noOfRows;
        private readonly int _noOfColumns;
        private readonly bool[] _values = { true, false };

        public BoardArrayStrategyRandom(int noOfRows, int noOfColumns)
        {
            _noOfRows = noOfRows;
            _noOfColumns = noOfColumns;
        }

        public bool[,] GenerateBoardArray()
        {
            var boardArray = new bool[_noOfRows, _noOfColumns];
            
            AssignRandomValuesToTiles(boardArray);

            return boardArray;
        }

        private void AssignRandomValuesToTiles(bool[,] boardArray)
        {
            // Random must be created here otherwise will return same values in loop
            var rand = new Random();

            for (var row = 0; row < _noOfRows; row++)
            {
                for (var col = 0; col < _noOfColumns; col++)
                {
                    boardArray[row, col] = _values[rand.Next(0, _values.Length)];
                }
            }
        }
    }
}
