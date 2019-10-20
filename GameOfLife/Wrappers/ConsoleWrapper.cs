using System;
using System.Diagnostics.CodeAnalysis;

namespace GameOfLife.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class ConsoleWrapper : IConsole
    {
        public ConsoleWrapper()
        {
        }

        public bool KeyAvailable => Console.KeyAvailable;

        public void Clear() => Console.Clear();

        public void WriteLine(string message) => Console.WriteLine(message);

        public void Write(string message) => Console.Write(message);

        public ConsoleKey ReadKey(bool intercept) => Console.ReadKey(intercept).Key;

        public ConsoleKeyInfo GetConsoleKeyInfoFromReadKey() => Console.ReadKey(true);

        public ConsoleKey GetConsoleKey(ConsoleKeyInfo consoleKeyInfo) => consoleKeyInfo.Key;

        public string GetKeyCharToString(ConsoleKeyInfo consoleKeyInfo) => consoleKeyInfo.KeyChar.ToString();
    }
}
