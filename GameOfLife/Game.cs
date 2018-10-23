using System;
using System.IO;
using System.Reflection;

namespace GameOfLife
{
    public class Game
    {
        private readonly string[] _files;
        private int _option;

        public Game()
        {
            var folder = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) ??
                                 throw new InvalidOperationException()).LocalPath;
            _files = Directory.GetFiles(folder + "\\patterns", "*.txt");
        }

        public void NewGame()
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
            Console.WriteLine("Use \"O\" for alive tiles and \"-\" for dead tiles.");
            Console.WriteLine();

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

        public void SetBoard(Board board)
        {
            board.Set(_option == _files.Length
                ? new BoardGenerator().GenerateRandom(Constants.BoardRows, Constants.BoardColumns)
                : new BoardGenerator().GenerateFromFile(_files[_option]));
        }
    }
}
