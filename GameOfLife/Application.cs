using System;
using System.Threading;

namespace GameOfLife
{
    public class Application
    {
        private readonly IGame _game;
        private readonly IBoard _board;
        private readonly IBoardGenerator _boardGenerator;
        private readonly IConsole _console;

        public Application(IGame game, IBoard board, IBoardGenerator boardGenerator, IConsole console)
        {
            _game = game;
            _board = board;
            _boardGenerator = boardGenerator;
            _console = console;
        }
        public void Run()
        {
            do
            {
                _game.NewGame();
                
                if (!_game.SetOption())
                    break;
                
                _game.SetBoard(_board, _boardGenerator);

                do
                {
                    while (!_console.KeyAvailable)
                    {
                        _board.Evolve();
                        _board.Print();
                        Thread.Sleep(150);
                    }
                } while (_console.ReadKey(true) != ConsoleKey.Escape);

            } while (true);
        }        
    }
}
