using System.Collections.Generic;

namespace GameOfLife.IO
{
    public interface IFileNamesProvider
    {
        IReadOnlyList<string> GetFileNamesForPatternFiles();
    }
}