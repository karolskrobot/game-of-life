namespace GameOfLife.IO
{
    public interface IOptionKeyReader
    {
        Option GetOptionFromKeyPress(int optionRandomPosition);
    }
}