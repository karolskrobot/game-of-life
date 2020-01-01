using GameOfLife.BoardArrayGeneration;

namespace GameOfLife
{
    public interface IBoardFactory
    {
        IBoard CreateBoard(IBoardArrayStrategy boardArrayStrategy);
    }
}