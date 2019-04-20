using GameOfLife.Wrappers;

namespace GameOfLife
{
    public interface IGame
    {
        void NewGame();
        bool SetOption();
        void NewBoard(IBoardProcessor boardProcessor, IBoardGenerator boardGenerator, IFile fileWrapper);
    }
}