namespace GameOfLife
{
    public interface IGame
    {
        void NewGame();
        void SetOption();
        void SetBoard(IBoard board, IBoardGenerator boardGenerator);
    }
}