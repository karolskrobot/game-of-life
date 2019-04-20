namespace GameOfLife
{
    public interface IBoardProcessor
    {
        IBoard Board { get; set; }
        void EvolveBoard();
        void PrintBoard();
    }
}