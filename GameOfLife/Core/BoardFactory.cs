using GameOfLife.BoardArrayStrategies;

namespace GameOfLife.Core
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
