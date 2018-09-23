using System;

namespace GameOfLife
{
    public static class BoardIterator
    {
        public static void ByTile(bool[,] board, Func<bool, bool> func)
        {
            for (var i = 0; i < board.GetLength(0); i++)
                for (var j = 0; j < board.GetLength(1); j++)
                    board[i, j] = func(board[i, j]);
        }

        public static void ByCoords(bool[,] board, Func<int, int, bool> func)
        {
            for (var i = 0; i < board.GetLength(0); i++)
                for (var j = 0; j < board.GetLength(1); j++)
                    board[i, j] = func(i, j);
        }
    }
}