using System;
using System.IO;
using System.Linq;

namespace GameOfLife
{
    public class BoardGenerator
    {
        private readonly bool[] _values = { true, false };
        private bool[,] _board;

        public bool[,] GenerateRandom(int noRows, int noColumns)
        {
            _board = new bool[noRows, noColumns];

            // rand must be created here otherwise will return same values in loop
            var rand = new Random();

            BoardIterator.Iterate(_board, tile => _values[rand.Next(0, _values.Length)]);

            return _board;
        }

        public bool[,] GenerateFromFile(string path)
        {
            var rows = File.ReadAllText(path).Split('\n');
            _board = new bool[rows.Length, rows.Select(row => row.Length).ToList().Max()];

            var i = 0;
            foreach (var row in rows)
            {
                for (var j = 0; j < row.Length; j++)
                    switch (row[j].ToString())
                    {
                        case Constants.DeadChar:
                            _board[i, j] = false;
                            break;
                        case Constants.AliveChar:
                            _board[i, j] = true;
                            break;
                    }
                i++;
            }

            return _board;
        }
    }
}