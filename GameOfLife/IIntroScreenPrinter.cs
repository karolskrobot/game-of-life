using System.Collections.Generic;

namespace GameOfLife
{
    public interface IIntroScreenPrinter
    {
        void PrintNewGameScreen(IReadOnlyList<string> fileNames);
    }
}