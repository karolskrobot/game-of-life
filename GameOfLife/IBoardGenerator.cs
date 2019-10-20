using GameOfLife.BoardGenerationStrategies;

namespace GameOfLife
{
    public interface IBoardGenerator
    {
        IBoard GenerateBoard(IBoardGenerationStrategy boardGenerationStrategy);
    }
}
