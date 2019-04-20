namespace GameOfLife
{
    public class Board : IBoard
    {
        public bool[,] BoardArray { get; set; }

        public int LengthX => BoardArray.GetLength(0);

        public int LengthY => BoardArray.GetLength(1);
    }
}
