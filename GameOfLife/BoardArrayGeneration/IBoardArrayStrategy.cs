namespace GameOfLife.BoardArrayGeneration
{
    public interface IBoardArrayStrategy
    {
        bool[,] GenerateBoardArray();
    }
}