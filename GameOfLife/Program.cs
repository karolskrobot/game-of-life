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
            game.SetOption();

            var board = new Board();
            game.SetBoard(board);

            Console.Clear();
            board.Print();
            while (true)
            {
                Thread.Sleep(200);
                Console.Clear();
                board.Evolve();
                board.Print();
            }
        }
    }
}