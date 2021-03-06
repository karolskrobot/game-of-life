﻿using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GameOfLife.IO.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class FileWrapper : IFile
    {
        public FileWrapper()
        {
        }

        public string ReadAllText(string path) => File.ReadAllText(path);
    }
}
