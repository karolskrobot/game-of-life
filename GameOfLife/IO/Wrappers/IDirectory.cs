using System.Collections.Generic;

namespace GameOfLife.IO.Wrappers
{
    public interface IDirectory
    {
        IReadOnlyList<string> GetFileNames(string path, string searchPattern);
    }
}