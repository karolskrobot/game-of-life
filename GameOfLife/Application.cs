using GameOfLife.Wrappers;
using System;
using System.Threading;

namespace GameOfLife
{
    public class Application
    {
        private readonly IGame _game;
        private readonly IBoardProcessor _boardProcessor;
        private readonly IBoardGenerator _boardGenerator;
        private readonly IConsole _console;
        private readonly IFile _fileWrapper;

        public Application(
            IGame game, 
            IBoardProcessor boardProcessor, 
            IBoardGenerator boardGenerator, 
            IConsole console, 
            IFile fileWrapper)
        {
            _game = game;
            _boardProcessor = boardProcessor;
            _boardGenerator = boardGenerator;
            _console = console;
            _fileWrapper = fileWrapper;
        }
        public void Run()
        {
            do
            {
                _game.NewGame();
                
                if (!_game.SetOption())
                    break;
                
                _game.NewBoard(_boardProcessor, _boardGenerator, _fileWrapper);

                do
                {
                    while (!_console.KeyAvailable)
                    {
                        _boardProcessor.EvolveBoard();
                        _boardProcessor.PrintBoard();
                        Thread.Sleep(150);
                    }
                } while (_console.ReadKey(true) != ConsoleKey.Escape);

            } while (true);
        }        
    }
}
