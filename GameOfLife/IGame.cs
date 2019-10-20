using GameOfLife.Wrappers;

namespace GameOfLife
{
    public interface IGame
    {
        void PrintNewGameScreen();

        bool LoopReadingOptionKeyPressedReturnFalseWhenExit();

        IBoard GenerateNewBoard(IBoardGenerator boardGenerator, IFile fileWrapper);
    }
}