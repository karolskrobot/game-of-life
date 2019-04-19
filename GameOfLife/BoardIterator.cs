using System;
using System.Diagnostics.CodeAnalysis;

namespace GameOfLife
{
    [ExcludeFromCodeCoverage]
    public static class BoardIterator
    {
        /// <summary>
        /// Method looping through each element of 2d array and processing element in delegate method.
        /// </summary>
        /// <param name="board"> 2D array of bools representing the board.</param>
        /// <param name="func"> Delegate run for each element of array, outputting the processed value of current element.</param>
        public static void Iterate(bool[,] board, Func<bool, bool> func)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            for (var j = 0; j < board.GetLength(1); j++)
                board[i, j] = func(board[i, j]);
        }

        /// <summary>
        /// Delegate function which will be run for each element in 2d array.
        /// </summary>
        /// <param name="board"> 2D array of bools representing the board.</param>
        /// <param name="func"> Delegate run for each element of array, accepting row index and column index, processing the element with these coordinates, and outputting as bool value</param>
        public static void Iterate(bool[,] board, Func<int, int, bool> func)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            for (var j = 0; j < board.GetLength(1); j++)
                board[i, j] = func(i, j);
        }
    }
}