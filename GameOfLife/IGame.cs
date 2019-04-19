namespace GameOfLife
{
    public interface IGame
    {
        void NewGame();
        bool SetOption();
        void SetBoard(IBoard board, IBoardGenerator boardGenerator);
    }
}