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
        private bool _exit;

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
                _console.Clear();
                _game.NewGame();
                CheckOption();

                if (_exit)
                    break;

                do
                {
                    while (!_console.KeyAvailable)
                    {
                        RenderBoard();
                    }
                } while (_console.ReadKey(true) != ConsoleKey.Escape);

            } while (true);
        }

        private void CheckOption()
        {
            if (_game.SetOption())
            {
                _game.SetBoard(_board, _boardGenerator);
            }
            else
            {
                _exit = true;
            }
        }

        private void RenderBoard()
        {
            _console.Clear();
            _board.Evolve();
            _board.Print();
            Thread.Sleep(150);
        }
    }
}
