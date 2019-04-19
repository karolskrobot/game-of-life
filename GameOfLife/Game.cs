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
        private string[] _files;
        private int _option;

        public Game()
        {
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
                Console.WriteLine(i + 1 + ": " + Path.GetFileNameWithoutExtension(_files[i]));

            Console.WriteLine(_files.Length + 1 + ": Random");
            Console.Write("Enter number to load board:");
        }

        public void SetOption()
        {
            while (true)
            {
                if (!Int32.TryParse(Console.ReadLine(), out var option) || option < 1 || option > _files.Length + 1)
                    Console.WriteLine("Wrong input. Try again.");
                else
                {
                    option--;
                    _option = option;
                    return;
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
            Console.WriteLine();
            Console.WriteLine("CONWAY'S GAME OF LIFE");
            Console.WriteLine();
            Console.WriteLine("In this game tiles die and come alive with every generation.");
            Console.WriteLine("Dead tile will come alive if it has exactly 3 alive neighbours.");
            Console.WriteLine("Alive tile will die of loneliness if it has 0 or 1 alive neighbours.");
            Console.WriteLine("Alive tile will also die of overcrowding if it has more than 3 alive neighbours.");
            Console.WriteLine("You can read more about the game at https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life");
            Console.WriteLine();
            Console.WriteLine(
                "Choose a pattern or create a pattern yourself (save it in a .txt file in the patterns folder)");
            Console.WriteLine($"Use \"{Constants.AliveChar}\" for alive tiles and \"{Constants.DeadChar}\" for dead tiles.");
            Console.WriteLine();
        }
    }
}
