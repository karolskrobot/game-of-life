using GameOfLife.Wrappers;
using System.Linq;

namespace GameOfLife.BoardGenerationStrategies
{
    public class BoardGenerationStrategyFromFile : IBoardGenerationStrategy
    {
        private readonly string _path;
        private readonly IFile _fileWrapper;

        public BoardGenerationStrategyFromFile(string path, IFile fileWrapper)
        {
            _path = path;
            _fileWrapper = fileWrapper;
        }

        public IBoard Generate()
        {
            string[] rowStrings = GetRowStringsFromFile(_path, _fileWrapper);

            var board = new Board()
            {
                BoardArray = new bool[rowStrings.Length, GetNumberOfColumnsFromLongestRowString(rowStrings)]
            };
            
            AssignValuesToBoardTilesFromRowStrings(rowStrings, board);
            
            return board;
        }

        private void AssignValuesToBoardTilesFromRowStrings(string[] rowStrings, IBoard board)
        {
            for (var row = 0; row < rowStrings.Length; row++)
            {
                string rowString = rowStrings[row];

                for (var col = 0; col < rowString.Length; col++)
                {
                    if (rowString[col].ToString() == Constants.DeadChar)
                    {
                        board.SetTileValue(row, col, false);
                    }

                    if (rowString[col].ToString() == Constants.AliveChar)
                    {
                        board.SetTileValue(row, col, true);
                    }
                }
            }
        }

        private static int GetNumberOfColumnsFromLongestRowString(string[] rows) 
            => rows.Select(row => row.Length)
                    .ToList()
                    .Max();

        private static string[] GetRowStringsFromFile(string path, IFile fileWrapper) => fileWrapper.ReadAllText(path).Split('\n');
    }
}
