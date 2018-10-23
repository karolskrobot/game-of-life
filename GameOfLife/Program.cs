using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main()
        {
            var game = new Game();

            while (true)
            {
                Console.Clear();
                game.NewGame();
                game.SetOption();

                var board = new Board();
                game.SetBoard(board);

                do
                {
                    while (!Console.KeyAvailable)
                    {
                        Console.Clear();
                        board.Evolve();
                        board.Print();
                        Thread.Sleep(150);
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
        }
    }
}