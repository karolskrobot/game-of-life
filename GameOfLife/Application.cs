using GameOfLife.BoardArrayStrategies;
using GameOfLife.Core;
using GameOfLife.IO;
using GameOfLife.IO.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GameOfLife
{
    public class Application
    {
        private readonly IOptionKeyReader _optionKeyReader;
        private readonly IBoardFactory _boardFactory;
        private readonly IConsole _console;
        private readonly IConsolePrinter _consolePrinter;
        
        private readonly IReadOnlyList<string> _fileNames;
        private readonly IFile _fileWrapper;

        public Application(
            IConsole console,
            IConsolePrinter consolePrinter,
            IFileNamesProvider fileNamesProvider,
            IOptionKeyReader optionKeyReader,
            IBoardFactory boardFactory, 
            IFile fileWrapper)
        {
            _console = console;
            _consolePrinter = consolePrinter;
            _optionKeyReader = optionKeyReader;
            _boardFactory = boardFactory;
            _fileWrapper = fileWrapper;

            _fileNames = fileNamesProvider.GetFileNamesForPatternFiles();
        }

        public void Run()
        {
            do
            {
                _consolePrinter.PrintNewGameScreen(_fileNames);

                var optionRandomPosition = _fileNames.Count + 1; // random is the last option rendered after all filenames
                var optionRead = _optionKeyReader.GetOptionFromKeyPress(optionRandomPosition);

                if (optionRead.OptionType == OptionType.Exit)
                {
                    break;
                }

                var board = CreateBoard(optionRead);
                PrintBoardWithDelay(board);
                LoopEvolveAndRenderBoardUntilEscapePressed(board);

            } while (true);
        }

        private IBoard CreateBoard(Option optionRead)
        {
            var boardArrayStrategy = CreateBoardArrayStrategy(optionRead);
            return _boardFactory.CreateBoard(boardArrayStrategy);
        }

        private IBoardArrayStrategy CreateBoardArrayStrategy(Option option)
        {
            if (option.OptionType == OptionType.Random)
            {
                return new BoardArrayStrategyRandom(Constants.BoardRows, Constants.BoardColumns);
            }

            if (option.OptionType == OptionType.FromFile)
            {
                return new BoardArrayStrategyFromFile(_fileNames[option.FileNameCollectionPosition], _fileWrapper);
            }

            throw new NotSupportedException("Unable to generate new board. Incorrect option type.");
        }

        private void PrintBoardWithDelay(IBoard board)
        {
            Thread.Sleep(100);
            _consolePrinter.PrintBoard(board);
        }

        private void LoopEvolveAndRenderBoardUntilEscapePressed(IBoard board)
        {
            do
            {
                while (!_console.KeyAvailable)
                {
                    board.Evolve();
                    PrintBoardWithDelay(board);
                }
            } while (_console.ReadKey(true) != ConsoleKey.Escape);
        }
    }
}
