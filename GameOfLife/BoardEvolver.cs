using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class BoardEvolver : IBoardEvolver
    {
        public BoardEvolver()
        {
        }

        public void EvolveBoard(IBoard board)
        {
            var secondBoard = GetSecondBoardWithEvolvedValues(board);

            SetOriginalBoardArrayToSecondBoardArray(board, secondBoard);
        }

        private IBoard GetSecondBoardWithEvolvedValues(IBoard board)
        {
            var secondBoard = new Board
            {
                BoardArray = new bool[board.LengthRows, board.LengthColumns]
            };

            EvolveSecondBoardValues(board, secondBoard);

            return secondBoard; 
        }

        private void EvolveSecondBoardValues(IBoard board, IBoard secondBoard)
        {
            for (var row = 0; row < board.LengthRows; row++)
            {
                for (var col = 0; col < board.LengthColumns; col++)
                {
                    IEnumerable<bool> neighbours = board.GetTileNeighbours(row, col);

                    int aliveNeighboursCount = GetAliveNeighboursCount(neighbours);

                    bool centerTileValue = board.GetTileValue(row, col);

                    if (centerTileValue == false && aliveNeighboursCount == 3)
                    {
                        secondBoard.SetTileValue(row, col, true);
                    }
                    else if (centerTileValue == true && (aliveNeighboursCount < 2 || aliveNeighboursCount > 3))
                    {
                        secondBoard.SetTileValue(row, col, false);
                    }
                    else
                    {
                        secondBoard.SetTileValue(row, col, centerTileValue);
                    }
                }
            }
        }

        private static void SetOriginalBoardArrayToSecondBoardArray(IBoard board, IBoard secondBoard) 
            => board.BoardArray = secondBoard.BoardArray;

        private int GetAliveNeighboursCount(IEnumerable<bool> neighbours) => neighbours.Count(n => n == true);
    }
}
