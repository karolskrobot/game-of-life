using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GameOfLife.IO.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class DirectoryWrapper : IDirectory
    {
        public DirectoryWrapper()
        {
        }

        public IReadOnlyList<string> GetFileNames(string path, string searchPattern) 
            => Array.AsReadOnly(Directory.GetFiles(path, searchPattern));
    }
}
