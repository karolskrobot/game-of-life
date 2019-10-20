using GameOfLife;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BoardPrinterTests
    {
        private Mock<IConsole> _fakeConsole;

        [SetUp]
        public void SetUp()
        {
            _fakeConsole = new Mock<IConsole>();
        }

        [Test]
        public void Print_GivenBoolArray_PrintCorrectNumberOfCharacters()
        {
            //Arrange
            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };
            var board = new Board()
            {
                BoardArray = boardArray
            };

            var boardPrinter = new BoardPrinter(_fakeConsole.Object);

            //Act
            boardPrinter.PrintBoard(board);

            //Assert
            _fakeConsole.Verify(c => c.Clear(), Times.Once);
            _fakeConsole.Verify(c => c.Write(It.Is<string>(a => a.ToString() == Constants.AliveChar)), Times.Exactly(3));
            _fakeConsole.Verify(c => c.Write(It.Is<string>(a => a.ToString() == Constants.DeadChar)), Times.Exactly(22));
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == string.Empty)), Times.Exactly(7));
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "Press ESC to return.")), Times.Once);
            _fakeConsole.VerifyNoOtherCalls();
        }
    }
}
