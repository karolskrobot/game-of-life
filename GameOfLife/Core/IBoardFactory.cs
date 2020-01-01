using GameOfLife.BoardArrayStrategies;

namespace GameOfLife.Core
{
    public interface IBoardFactory
    {
        IBoard CreateBoard(IBoardArrayStrategy boardArrayStrategy);
    }
}