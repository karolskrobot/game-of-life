using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main()
        {

            var game = new Game();
            game.NewGame();
            game.SetOption(Console.ReadLine());

            var boardProcessor = new BoardProcessor();

            try
            {
                boardProcessor.SetBoard(game.Option == game.Files.Length
                    ? new BoardGenerator().GenerateRandom(Constants.BoardRows, Constants.BoardColumns)
                    : new BoardGenerator().GenerateFromFile(game.Files[game.Option]));
            }
            catch (Exception)
            {
                Console.WriteLine("Error.");
            }

            Console.Clear();
            boardProcessor.PrintBoard();

            while (true)
            {
                Thread.Sleep(200);
                Console.Clear();
                boardProcessor.EvolveBoard();
                boardProcessor.PrintBoard();
            }
        }
    }
}