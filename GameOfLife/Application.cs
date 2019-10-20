using GameOfLife.BoardGenerationStrategies;
using GameOfLife.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GameOfLife
{
    public class Application
    {
        private readonly IOptionKeyReader _optionKeyReader;
        private readonly IConsole _console;
        private readonly IFile _fileWrapper;
        private readonly IIntroScreenPrinter _introScreenPrinter;
        private readonly IFileNamesProvider _fileNamesProvider;
        private readonly IBoardEvolver _boardEvolver;
        private readonly IBoardGenerator _boardGenerator;
        private readonly IBoardPrinter _boardPrinter;
        private readonly IReadOnlyList<string> _fileNames;

        public Application(
            IConsole console,
            IFile fileWrapper,
            IIntroScreenPrinter introScreenPrinter,
            IFileNamesProvider fileNamesProvider,
            IOptionKeyReader optionKeyReader,
            IBoardEvolver boardEvolver, 
            IBoardGenerator boardGenerator, 
            IBoardPrinter boardPrinter
        )
        {
            _optionKeyReader = optionKeyReader;
            _console = console;
            _fileWrapper = fileWrapper;
            _introScreenPrinter = introScreenPrinter;
            _fileNamesProvider = fileNamesProvider;
            _boardEvolver = boardEvolver;
            _boardGenerator = boardGenerator;
            _boardPrinter = boardPrinter;

            _fileNames = _fileNamesProvider.GetFileNamesForPatternFiles();
        }

        public void Run()
        {
            do
            {
                _introScreenPrinter.PrintNewGameScreen(_fileNames);

                var optionRead = _optionKeyReader.GetOptionFromKeyPress(_fileNames.Count);

                if (optionRead.OptionType == OptionType.Exit)
                {
                    break;
                }
                    
                var board = GenerateNewBoard(optionRead);

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
            Thread.Sleep(100);
            _boardPrinter.PrintBoard(board);
        }

        private IBoard GenerateNewBoard(Option option)
        {
            if (option.OptionType == OptionType.Random)
            {
                var boardGenerationStrategyRandom = 
                    new BoardGenerationStrategyRandom(Constants.BoardRows, Constants.BoardColumns);

                return _boardGenerator.GenerateBoard(boardGenerationStrategyRandom);
            }

            if (option.OptionType == OptionType.FromFile)
            {
                var boardGenerationStrategyFromFile =
                    new BoardGenerationStrategyFromFile(_fileNames[option.FileNameCollectionPosition], _fileWrapper);

                return _boardGenerator.GenerateBoard(boardGenerationStrategyFromFile);
            }

            throw new NotSupportedException("Unable to generate new board. Incorrect option type.");
        }

    }
}
