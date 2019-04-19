using System;

namespace GameOfLife
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