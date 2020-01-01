namespace GameOfLife.IO.Wrappers
{
    public interface IFile
    {
        string ReadAllText(string path);
    }
}