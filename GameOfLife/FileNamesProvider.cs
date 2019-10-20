using GameOfLife.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GameOfLife
{
    public class FileNamesProvider : IFileNamesProvider
    {
        private readonly IDirectory _directoryWrapper;
        private readonly IReadOnlyList<string> _fileNames;

        public FileNamesProvider(IDirectory directoryWrapper)
        {
            _directoryWrapper = directoryWrapper;

            var folder = GetFolderOfExecutingAssembly();

            _fileNames = GetFileNamesFromPatternFilesFolder(folder);
        }

        public IReadOnlyList<string> GetFileNamesForPatternFiles() => _fileNames;

        private string GetFolderOfExecutingAssembly() =>
            new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException()).LocalPath;

        private IReadOnlyList<string> GetFileNamesFromPatternFilesFolder(string folder) => _directoryWrapper.GetFileNames($"{folder}\\patterns", "*.txt");
    }
}
