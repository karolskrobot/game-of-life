using System;

namespace GameOfLife.Wrappers
{
    public interface IConsole
    {
        bool KeyAvailable { get; }

        void Clear();

        void WriteLine(string message);

        void Write(string message);

        ConsoleKey ReadKey(bool intercept);

        ConsoleKeyInfo GetConsoleKeyInfoFromReadKey();

        ConsoleKey GetConsoleKey(ConsoleKeyInfo consoleKeyInfo);

        string GetKeyCharToString(ConsoleKeyInfo consoleKeyInfo);
    }
}