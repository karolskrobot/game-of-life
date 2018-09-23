using System;
using System.IO;

namespace GameOfLife
{
    public class Game
    {
        public string[] Files { get; }
        public int Option { get; private set; }

        public Game()
        {
            var folder = @"D:\_programming projects\GameOfLife\GameOfLife\patterns\";
            Files = Directory.GetFiles(folder, "*.txt");
        }

        public void NewGame()
        {
            Console.WriteLine();
            Console.WriteLine("CONWAY'S GAME OF LIFE");
            Console.WriteLine();
            Console.WriteLine("In this game tiles die and come alive with every generation.");
            Console.WriteLine("A dead tile will come alive if it has exactly 3 alive neighbours.");
            Console.WriteLine("An alive tile will die of loneliness if it has 0 or 1 alive neighbours.");
            Console.WriteLine("An alive tile will also die of overcrowding if it has more than 3 alive neighbours.");
            Console.WriteLine();
            Console.WriteLine(
                "Choose a pattern or create a pattern yourself (save it in a .txt file in the patterns folder)");
            Console.WriteLine("Use \"O\" for alive tiles and \"-\" for dead tiles.");
            Console.WriteLine();

            for (var i = 0; i < Files.Length; i++)
                Console.WriteLine(i + 1 + ": " + Path.GetFileNameWithoutExtension(Files[i]));

            Console.WriteLine(Files.Length + 1 + ": Random");
            Console.Write("Enter number to load board: ");
        }

        public void SetOption(string input)
        {
            int.TryParse(input, out var option);
            option--;
            Option = option;
        }

    }
}
