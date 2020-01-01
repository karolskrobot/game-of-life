using System.Collections.Generic;

namespace GameOfLife
{
    public interface IConsolePrinter
    {
        void PrintNewGameScreen(IReadOnlyList<string> fileNames);

        void PrintBoard(IBoard board);
    }
}