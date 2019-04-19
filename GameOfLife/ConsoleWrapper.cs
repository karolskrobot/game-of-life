using System;

namespace GameOfLife
{
    public class ConsoleWrapper : IConsole
    {
        public bool KeyAvailable => Console.KeyAvailable;

        public void Clear()
        {
            Console.Clear();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public ConsoleKey ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept).Key;
        }       
    }
}
