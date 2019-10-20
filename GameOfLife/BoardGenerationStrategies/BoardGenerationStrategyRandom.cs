using System;

namespace GameOfLife.BoardGenerationStrategies
{
    public class BoardGenerationStrategyRandom : IBoardGenerationStrategy
    {
        private readonly int _noOfRows;
        private readonly int _noOfColumns;
        private readonly bool[] _values = { true, false };

        public BoardGenerationStrategyRandom(int noOfRows, int noOfColumns)
        {
            _noOfRows = noOfRows;
            _noOfColumns = noOfColumns;
        }

        public IBoard Generate()
        {
            var board = new Board
            {
                BoardArray = new bool[_noOfRows, _noOfColumns]
            };
            
            AssignRandomValuesToTiles(board);

            return board;
        }

        private void AssignRandomValuesToTiles(IBoard board)
        {
            // rand must be created here otherwise will return same values in loop
            var rand = new Random();

            for (var row = 0; row < board.LengthRows; row++)
            {
                for (var col = 0; col < board.LengthColumns; col++)
                {
                    board.SetTileValue(row, col, _values[rand.Next(0, _values.Length)]);
                }
            }
        }
    }
}
