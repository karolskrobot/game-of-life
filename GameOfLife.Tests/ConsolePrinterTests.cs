using GameOfLife.Core;
using GameOfLife.IO;
using GameOfLife.IO.Wrappers;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class ConsolePrinterTests
    {
        private Mock<IConsole> _fakeConsole;
        private string[] _fileNames;

        [SetUp]
        public void SetUp()
        {
            _fakeConsole = new Mock<IConsole>();
            _fileNames = new[] {"a.txt", "b.txt"};
        }

        [Test]
        public void PrintNewGameScreen_GivenArrayOfFileNames_WritesCorrectOptionsLinesToTheConsole()
        {
            //Arrange
            var introScreenPrinter  = new ConsolePrinter(_fakeConsole.Object);

            //Act
            introScreenPrinter.PrintNewGameScreen(_fileNames);

            //Assert
            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(output => output.ToString() == "1: A")), Times.Once);
            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(output => output.ToString() == "2: B")), Times.Once);
            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(output => output.ToString() == "3: Random")), Times.Once);
        }

        [Test]
        public void PrintBoard_GivenBoolArray_PrintCorrectNumberOfCharacters()
        {
            //Arrange
            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };
            var board = new Board(boardArray);

            var boardPrinter = new ConsolePrinter(_fakeConsole.Object);

            //Act
            boardPrinter.PrintBoard(board);

            //Assert
            _fakeConsole.Verify(_ => _.Clear(), Times.Once);
            _fakeConsole.Verify(_ => _.Write(It.Is<string>(output => output.ToString() == Constants.AliveChar)), Times.Exactly(3));
            _fakeConsole.Verify(_ => _.Write(It.Is<string>(output => output.ToString() == Constants.DeadChar)), Times.Exactly(22));
            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(output => output.ToString() == string.Empty)), Times.Exactly(7));
            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(output => output.ToString() == "Press ESC to return.")), Times.Once);
            _fakeConsole.VerifyNoOtherCalls();
        }
    }
}
