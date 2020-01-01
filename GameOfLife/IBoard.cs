namespace GameOfLife
{
    public interface IBoard
    {
        int LengthRows { get; }

        int LengthColumns { get; }

        bool GetTileValue(int row, int column);

        void Evolve();
    }
}