using System;
using System.Threading;

namespace GameOfLife
{
    public class Application
    {
        private readonly IGame _game;
        private readonly IBoard _board;
        private readonly IBoardGenerator _boardGenerator;

        public Application(IGame game, IBoard board, IBoardGenerator boardGenerator)
        {
            _game = game;
            _board = board;
            _boardGenerator = boardGenerator;
        }
        public void Run()
        {            
            while (true)
            {
                Console.Clear();
                _game.NewGame();
                _game.SetOption();
                
                _game.SetBoard(_board, _boardGenerator);

                do
                {
                    while (!Console.KeyAvailable)
                    {
                        Console.Clear();
                        _board.Evolve();
                        _board.Print();
                        Thread.Sleep(150);
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
        }
    }
}
