using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GameOfLife
{
    public class Game : IGame
    {
        private const string ExitCharacter = "X";

        private string[] _files;
        private int _option;
        private IConsole _console;

        public Game(IConsole console)
        {
            _console = console;

            var folder = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                                 ?? throw new InvalidOperationException()).LocalPath;

            _files = Directory
                .GetFiles($"{folder}\\patterns", "*.txt")
                .Select(filename => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(filename))
                .ToArray();
        }

        public void NewGame()
        {
            IntroText();

            for (var i = 0; i < _files.Length; i++)
                _console.WriteLine(i + 1 + ": " + Path.GetFileNameWithoutExtension(_files[i]));

            _console.WriteLine(_files.Length + 1 + ": Random");            
            _console.WriteLine($"Enter number to load board or {ExitCharacter} for Exit:");
        }

        public bool SetOption()
        {            
            while (true)
            {
                var input = _console.ReadLine();

                if (string.Equals(input, ExitCharacter, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

                if (!Int32.TryParse(input, out var option) || option < 1 || option > _files.Length + 1)
                {
                    _console.WriteLine("Wrong input. Try again.");
                }
                else
                {
                    option--;
                    _option = option;
                    return true;
                }
            }
        }

        public void SetBoard(IBoard board, IBoardGenerator boardGenerator)
        {
            board.Set(_option == _files.Length
                ? boardGenerator.GenerateRandom(Constants.BoardRows, Constants.BoardColumns)
                : boardGenerator.GenerateFromFile(_files[_option]));
        }

        private void IntroText()
        {
            _console.WriteLine(string.Empty);
            _console.WriteLine("CONWAY'S GAME OF LIFE");
            _console.WriteLine(string.Empty);
            _console.WriteLine("In this game tiles die and come alive with every generation.");
            _console.WriteLine("Dead tile will come alive if it has exactly 3 alive neighbours.");
            _console.WriteLine("Alive tile will die of loneliness if it has 0 or 1 alive neighbours.");
            _console.WriteLine("Alive tile will also die of overcrowding if it has more than 3 alive neighbours.");
            _console.WriteLine("You can read more about the game at https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life");
            _console.WriteLine(string.Empty);
            _console.WriteLine(
                "Choose a pattern or create a pattern yourself (save it in a .txt file in the patterns folder)");
            _console.WriteLine($"Use \"{Constants.AliveChar}\" for alive tiles and \"{Constants.DeadChar}\" for dead tiles.");
            _console.WriteLine(string.Empty);
        }
    }
}
