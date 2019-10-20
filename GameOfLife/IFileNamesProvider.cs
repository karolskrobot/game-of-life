using System.Collections.Generic;

namespace GameOfLife
{
    public interface IFileNamesProvider
    {
        IReadOnlyList<string> GetFileNamesForPatternFiles();
    }
}