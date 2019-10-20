using GameOfLife.Wrappers;
using System;
using System.Threading;

namespace GameOfLife
{
    public class Application
    {
        private readonly IGame _game;
        private readonly IConsole _console;
        private readonly IFile _fileWrapper;
        private readonly IBoardEvolver _boardEvolver;
        private readonly IBoardGenerator _boardGenerator;
        private readonly IBoardPrinter _boardPrinter;

        public Application(
            IConsole console,
            IFile fileWrapper,
            IGame game,
            IBoardEvolver boardEvolver, 
            IBoardGenerator boardGenerator, 
            IBoardPrinter boardPrinter
        )
        {
            _game = game;
            _console = console;
            _fileWrapper = fileWrapper;
            _boardEvolver = boardEvolver;
            _boardGenerator = boardGenerator;
            _boardPrinter = boardPrinter;
        }
        public void Run()
        {
            do
            {
                _game.PrintNewGameScreen();

                if (!_game.LoopReadingOptionKeyPressedReturnFalseWhenExit())
                {
                    break; //exits application
                }
                    
                var board = _game.GenerateNewBoard(_boardGenerator, _fileWrapper);

                RenderBoardGeneration(board);
                LoopEvolveAndRenderBoardUntilEscapePressed(board);

            } while (true);
        }

        private void LoopEvolveAndRenderBoardUntilEscapePressed(IBoard board)
        {
            do
            {
                while (!_console.KeyAvailable)
                {
                    _boardEvolver.EvolveBoard(board);
                    RenderBoardGeneration(board);
                }
            } while (_console.ReadKey(true) != ConsoleKey.Escape);
        }

        private void RenderBoardGeneration(IBoard board)
        {
            _boardPrinter.PrintBoard(board);
            Thread.Sleep(100);
        }
    }
}
