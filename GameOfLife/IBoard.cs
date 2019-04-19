namespace GameOfLife
{
    public interface IBoard
    {
        void Set(bool[,] board);
        void Evolve();
        void Print();
    }
}