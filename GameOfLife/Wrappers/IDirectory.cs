using System.Collections.Generic;

namespace GameOfLife.Wrappers
{
    public interface IDirectory
    {
        IReadOnlyList<string> GetFileNames(string path, string searchPattern);
    }
}