using GameOfLife.Wrappers;

namespace GameOfLife
{
    public interface IBoardGenerator
    {
        IBoard GenerateRandom(int noRows, int noColumns);
        IBoard GenerateFromFile(string path, IFile file);
    }
}
