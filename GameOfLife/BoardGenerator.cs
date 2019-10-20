using GameOfLife.BoardGenerationStrategies;

namespace GameOfLife
{
    public class BoardGenerator : IBoardGenerator
    {
        public BoardGenerator()
        {
        }

        public IBoard GenerateBoard(IBoardGenerationStrategy boardGenerationStrategy)
        {
            return boardGenerationStrategy.Generate();
        }
    }
}