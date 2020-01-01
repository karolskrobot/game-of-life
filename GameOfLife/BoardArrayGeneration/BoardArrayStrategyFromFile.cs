using GameOfLife.Wrappers;
using System.Linq;

namespace GameOfLife.BoardArrayGeneration
{
    public class BoardArrayStrategyFromFile : IBoardArrayStrategy
    {
        private readonly string _path;
        private readonly IFile _fileWrapper;

        public BoardArrayStrategyFromFile(string path, IFile fileWrapper)
        {
            _path = path;
            _fileWrapper = fileWrapper;
        }

        public bool[,] GenerateBoardArray()
        {
            string[] rowStrings = GetRowStringsFromFile(_path);

            var boardArray = new bool[rowStrings.Length, GetNumberOfColumnsFromLongestRowString(rowStrings)];
            
            AssignValuesToBoardTilesFromRowStrings(rowStrings, boardArray);
            
            return boardArray;
        }

        private string[] GetRowStringsFromFile(string path) => _fileWrapper.ReadAllText(path).Split('\n');

        private int GetNumberOfColumnsFromLongestRowString(string[] rows) => rows.Select(row => row.Length).Max();

        private void AssignValuesToBoardTilesFromRowStrings(string[] rowStrings, bool[,] board)
        {
            for (var row = 0; row < rowStrings.Length; row++)
            {
                string rowString = rowStrings[row];

                for (var col = 0; col < rowString.Length; col++)
                {
                    if (rowString[col].ToString() == Constants.DeadChar)
                    {
                        board[row, col] = false;
                    }

                    if (rowString[col].ToString() == Constants.AliveChar)
                    {
                        board[row, col] = true;
                    }
                }
            }
        }
    }
}
