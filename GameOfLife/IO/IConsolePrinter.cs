using GameOfLife.Core;
using System.Collections.Generic;

namespace GameOfLife.IO
{
    public interface IConsolePrinter
    {
        void PrintNewGameScreen(IReadOnlyList<string> fileNames);

        void PrintBoard(IBoard board);
    }
}