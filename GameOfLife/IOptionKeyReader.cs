namespace GameOfLife
{
    public interface IOptionKeyReader
    {
        Option GetOptionFromKeyPress(int fileNamesCount);
    }
}