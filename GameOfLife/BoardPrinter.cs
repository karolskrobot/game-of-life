using GameOfLife.Wrappers;

namespace GameOfLife
{
    public class BoardPrinter : IBoardPrinter
    {
        private readonly IConsole _console;

        public BoardPrinter(IConsole console)
        {
            _console = console;
        }

        public void PrintBoard(IBoard board)
        {
            _console.Clear();
            _console.WriteLine(string.Empty);
            PrintTiles(board);
            _console.WriteLine(string.Empty);
            _console.WriteLine("Press ESC to return.");
        }

        private void PrintTiles(IBoard board)
        {
            for (var row = 0; row < board.LengthRows; row++)
            {
                for (var col = 0; col < board.LengthColumns; col++)
                {
                    var valueToPrint = board.GetTileValue(row, col);
                    PrintTile(valueToPrint);
                }

                _console.WriteLine(string.Empty);
            }
        }

        private void PrintTile(bool valueToPrint) => _console.Write(valueToPrint ? Constants.AliveChar : Constants.DeadChar);
    }
}
