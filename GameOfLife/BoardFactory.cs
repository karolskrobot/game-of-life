using GameOfLife.BoardArrayGeneration;

namespace GameOfLife
{
    public class BoardFactory : IBoardFactory
    {
        public IBoard CreateBoard(IBoardArrayStrategy boardArrayStrategy)
        {
            var boardArray = boardArrayStrategy.GenerateBoardArray();
            return new Board(boardArray);
        }
    }
}
