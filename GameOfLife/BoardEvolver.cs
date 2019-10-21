using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class BoardEvolver : IBoardEvolver
    {
        public BoardEvolver()
        {
        }

        public void EvolveBoard(IBoard originalBoard)
        {
            var newBoard = GetNewBoardWithEvolvedValues(originalBoard);

            SetOriginalBoardArrayToNewBoardArray(originalBoard, newBoard);
        }

        private IBoard GetNewBoardWithEvolvedValues(IBoard originalBoard)
        {
            var newBoard = new Board
            {
                BoardArray = new bool[originalBoard.LengthRows, originalBoard.LengthColumns]
            };

            EvolveNewBoardValues(originalBoard, newBoard);

            return newBoard; 
        }

        private void EvolveNewBoardValues(IBoard originalBoard, IBoard newBoard)
        {
            for (var row = 0; row < originalBoard.LengthRows; row++)
            {
                for (var col = 0; col < originalBoard.LengthColumns; col++)
                {
                    IEnumerable<bool> neighbours = originalBoard.GetTileNeighbours(row, col);

                    int aliveNeighboursCount = GetAliveNeighboursCount(neighbours);

                    bool centerTileValue = originalBoard.GetTileValue(row, col);

                    if (centerTileValue == false && aliveNeighboursCount == 3)
                    {
                        newBoard.SetTileValue(row, col, true);
                    }
                    else if (centerTileValue == true && (aliveNeighboursCount < 2 || aliveNeighboursCount > 3))
                    {
                        newBoard.SetTileValue(row, col, false);
                    }
                    else
                    {
                        newBoard.SetTileValue(row, col, centerTileValue);
                    }
                }
            }
        }

        private int GetAliveNeighboursCount(IEnumerable<bool> neighbours) => neighbours.Count(n => n == true);

        private static void SetOriginalBoardArrayToNewBoardArray(IBoard board, IBoard secondBoard) 
            => board.BoardArray = secondBoard.BoardArray;
    }
}
