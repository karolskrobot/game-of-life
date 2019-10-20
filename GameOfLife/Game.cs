using GameOfLife.BoardGenerationStrategies;
using GameOfLife.Wrappers;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace GameOfLife
{
    public class Game : IGame
    {
        private readonly IConsole _console;
        private readonly string[] _files;
        private readonly IDirectory _directoryWrapper;
        private int _option;

        public Game(IConsole console, IDirectory directoryWrapper)
        {
            _console = console;
            _directoryWrapper = directoryWrapper;

            var folder = GetFolderOfExecutingAssembly();
            _files = GetFilesFromPatternFilesFolder(folder);
        }

        private string GetFolderOfExecutingAssembly() =>
            new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException()).LocalPath;

        private string[] GetFilesFromPatternFilesFolder(string folder) => _directoryWrapper.GetFiles($"{folder}\\patterns", "*.txt");
        
        public void PrintNewGameScreen()
        {
            PrintIntroText();
            PrintPatternsFromFilesOptions();
            PrintOptionRandom();
            PrintChoiceText();
        }
        
        public bool LoopReadingOptionKeyPressedReturnFalseWhenExit()
        {            
            while (true)
            {
                ConsoleKeyInfo input = _console.GetConsoleKeyInfoFromReadKey();

                ConsoleKey keyPressed = _console.GetConsoleKey(input);

                if (GetIsOptionExit(keyPressed))
                {
                    return false;
                }

                var inputToString = _console.GetKeyCharToString(input);

                var (validKeyPressed, optionChosen) = GetIsValidKeyPressedAndOption(inputToString);

                if (!validKeyPressed)
                {
                    _console.WriteLine("Wrong input. Try again.");
                }
                else
                {
                    _option = --optionChosen;
                    return true;
                }
            }
        }

        private void PrintIntroText()
        {
            _console.Clear();
            _console.WriteLine(string.Empty);
            _console.WriteLine("CONWAY'S GAME OF LIFE");
            _console.WriteLine(string.Empty);
            _console.WriteLine("In this game tiles die and come alive with every generation.");
            _console.WriteLine("Dead tile will come alive if it has exactly 3 alive neighbours.");
            _console.WriteLine("Alive tile will die of loneliness if it has 0 or 1 alive neighbours.");
            _console.WriteLine("Alive tile will also die of overcrowding if it has more than 3 alive neighbours.");
            _console.WriteLine("You can read more about the game at https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life");
            _console.WriteLine(string.Empty);
            _console.WriteLine(
                "Choose a pattern or create a pattern yourself (save it in a .txt file in the patterns folder)");
            _console.WriteLine($"Use \"{Constants.AliveChar}\" for alive tiles and \"{Constants.DeadChar}\" for dead tiles.");
            _console.WriteLine(string.Empty);
        }
        
        private void PrintPatternsFromFilesOptions()
        {
            for (var i = 0; i < _files.Length; i++)
            {
                PrintPatternFromFileOption(i);
            }

        }
        
        private void PrintPatternFromFileOption(int i)
            => _console.WriteLine($"{i + 1}: {Path.GetFileNameWithoutExtension(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_files[i]))}");

        private void PrintOptionRandom() => _console.WriteLine($"{_files.Length + 1}: Random");

        private void PrintChoiceText() => _console.WriteLine("Enter number to load board or Escape for Exit.");

        private static bool GetIsOptionExit(ConsoleKey input) => input == ConsoleKey.Escape;

        private (bool ValidKeyPressed, int OptionChosen) GetIsValidKeyPressedAndOption(string input)
        {
            var canBeParsed = int.TryParse(input, out var option);

            var valid = canBeParsed && option >= 1 && option <= _files.Length + 1;

            return (valid, option);
        }

        public IBoard GenerateNewBoard(IBoardGenerator boardGenerator, IFile fileWrapper)
        {
            if (GetIsOptionRandom())
            {
                var boardGenerationStrategyRandom = new BoardGenerationStrategyRandom(Constants.BoardRows, Constants.BoardColumns);
                return boardGenerator.GenerateBoard(boardGenerationStrategyRandom);
            }
            else
            {
                var boardGenerationStrategyFromFile = new BoardGenerationStrategyFromFile(_files[_option], fileWrapper);
                return boardGenerator.GenerateBoard(boardGenerationStrategyFromFile);
            }
        }

        private bool GetIsOptionRandom() => _option == _files.Length;
    }
}
