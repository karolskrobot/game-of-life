using GameOfLife.Wrappers;
using System;
using System.Linq;

namespace GameOfLife
{
    public class BoardGenerator : IBoardGenerator
    {
        private readonly IBoard _board;
        private bool[,] _boardArray;
        private readonly bool[] _values = { true, false };
        
        public BoardGenerator(IBoard board)
        {
            _board = board;
        }
     
        public IBoard GenerateRandom(int noRows, int noColumns)
        {
            _boardArray = new bool[noRows, noColumns];

            // rand must be created here otherwise will return same values in loop
            var rand = new Random();

            BoardIterator.Iterate(_boardArray, tile => _values[rand.Next(0, _values.Length)]);

            _board.BoardArray = _boardArray;
            return _board;
        }

        public IBoard GenerateFromFile(string path, IFile fileWrapper)
        {
            var rows = fileWrapper.ReadAllText(path).Split('\n');

            _boardArray = new bool[rows.Length, rows.Select(row => row.Length).ToList().Max()];

            var i = 0;
            foreach (var row in rows)
            {
                for (var j = 0; j < row.Length; j++)
                    switch (row[j].ToString())
                    {
                        case Constants.DeadChar:
                            _boardArray[i, j] = false;
                            break;
                        case Constants.AliveChar:
                            _boardArray[i, j] = true;
                            break;
                    }
                i++;
            }

            _board.BoardArray = _boardArray;
            return _board;
        }
    }
}