namespace GameOfLife
{
    public interface IBoard
    {
        bool[,] BoardArray { get; set; }
        int LengthX { get; }
        int LengthY { get; }
    }
}