using GameOfLife;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class IntroScreenPrinterTests
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
        public void NewGame_MethodCalled_WritesCorrectOptionsLinesToTheConsole()
        {
            //Arrange
            var introScreenPrinter  = new IntroScreenPrinter(_fakeConsole.Object);

            //Act
            introScreenPrinter.PrintNewGameScreen(_fileNames);

            //Assert
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "1: A")), Times.Once);
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "2: B")), Times.Once);
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "3: Random")), Times.Once);
        }
    }
}
