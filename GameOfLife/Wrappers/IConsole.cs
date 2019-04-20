using System;

namespace GameOfLife.Wrappers
{
    public interface IConsole
    {
        bool KeyAvailable { get; }
        void Clear();
        void WriteLine(string message);
        void Write(string message);
        string ReadLine();
        ConsoleKey ReadKey(bool intercept);
    }
}