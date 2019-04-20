namespace GameOfLife.Wrappers
{
    public interface IDirectory
    {
        string[] GetFiles(string path, string searchPattern);
    }
}