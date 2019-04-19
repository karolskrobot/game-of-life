namespace GameOfLife
{
    public interface IBoardGenerator
    {
        bool[,] GenerateRandom(int noRows, int noColumns);
        bool[,] GenerateFromFile(string path);
    }
}