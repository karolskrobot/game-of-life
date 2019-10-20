using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GameOfLife.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class DirectoryWrapper : IDirectory
    {
        public DirectoryWrapper()
        {
        }

        public string[] GetFiles(string path, string searchPattern) => Directory.GetFiles(path, searchPattern);
    }
}
