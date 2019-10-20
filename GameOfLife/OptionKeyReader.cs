using GameOfLife.Wrappers;
using System;

namespace GameOfLife
{
    public class OptionKeyReader : IOptionKeyReader
    {
        private readonly IConsole _console;

        public OptionKeyReader(IConsole console)
        {
            _console = console;
        }
        
        public Option GetOptionFromKeyPress(int fileNamesCount)
        {            
            var option = new Option();

            while (true)
            {
                ConsoleKeyInfo input = _console.GetConsoleKeyInfoFromReadKey();
                ConsoleKey keyPressed = _console.GetConsoleKey(input);

                if (GetIsOptionExit(keyPressed))
                {
                    option.OptionType = OptionType.Exit;
                    return option;
                }

                var inputToString = _console.GetKeyCharToString(input);

                (bool validKeyPressed, int optionValue) = GetIsValidKeyPressedAndOptionValue(inputToString, fileNamesCount);

                if (!validKeyPressed)
                {
                    _console.WriteLine("Wrong input. Try again.");
                }
                else if (GetIsOptionRandom(optionValue, fileNamesCount)) // random is the last option rendered after all filenames
                {
                    option.OptionType = OptionType.Random;
                    return option;
                }
                else
                {
                    option.OptionType = OptionType.FromFile;
                    SetPositionInFileNameCollection(option, optionValue); 
                    return option;
                }
            }
        }

        private static void SetPositionInFileNameCollection(Option option, int optionChosen) 
            => option.FileNameCollectionPosition = --optionChosen;

        private bool GetIsOptionExit(ConsoleKey input) => input == ConsoleKey.Escape;

        private (bool ValidKeyPressed, int OptionChosen) GetIsValidKeyPressedAndOptionValue(string input, int fileNamesCount)
        {
            var canBeParsed = int.TryParse(input, out var optionValue);

            var valid = canBeParsed && optionValue >= 1 && optionValue <= fileNamesCount + 1;

            return (valid, optionValue);
        }
        
        private bool GetIsOptionRandom(int optionValue, int fileNamesCount)
            => optionValue == fileNamesCount + 1;
    }
}
