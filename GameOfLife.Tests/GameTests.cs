using GameOfLife;
using GameOfLife.BoardGenerationStrategies;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;
using System;

namespace GameOfLifeTests
{
    [TestFixture]
    public class GameTests
    {        
        private Mock<IDirectory> _fakeDirectoryWrapper;
        private Mock<IConsole> _fakeConsole;
        private Mock<IBoardGenerator> _fakeBoardGenerator;
        private Mock<IFile> _fakeFileWrapper;

        [SetUp]
        public void SetUp()
        {
            _fakeDirectoryWrapper = new Mock<IDirectory>();
            _fakeConsole = new Mock<IConsole>();
            _fakeBoardGenerator = new Mock<IBoardGenerator>();
            _fakeFileWrapper = new Mock<IFile>();

            _fakeDirectoryWrapper
                .Setup(_ => _.GetFiles(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new[] {"a.txt", "b.txt"});
        }

        [Test]
        public void NewGame_MethodCalled_WritesCorrectOptionsLinesToTheConsole()
        {
            //Arrange
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);

            //Act
            game.PrintNewGameScreen();

            //Assert
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "1: A")), Times.Once);
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "2: B")), Times.Once);
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "3: Random")), Times.Once);
        }

        [Test]
        public void SetOption_ExitCharacterPressed_ReturnsFalse()
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.Setup(_ => _.GetConsoleKey(It.IsAny<ConsoleKeyInfo>())).Returns(ConsoleKey.Escape);

            var result = game.LoopReadingOptionKeyPressedReturnFalseWhenExit();

            Assert.That(result.Equals(false));
        }

        [Test]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public void SetOption_BoardChoiceCharacterPressed_ReturnsTrue(string boardChoice)
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.Setup(_ => _.GetKeyCharToString(It.IsAny<ConsoleKeyInfo>())).Returns(boardChoice);

            var result = game.LoopReadingOptionKeyPressedReturnFalseWhenExit();

            Assert.That(result.Equals(true));
        }

        [Test]
        public void SetOption_2IncorrectInputsUntilCorrect_WritesErrorMessage2TimesThenReturnsTrue()
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.SetupSequence(_ => _.GetKeyCharToString(It.IsAny<ConsoleKeyInfo>()))
                .Returns("A")
                .Returns("4")
                .Returns("1");

            var result = game.LoopReadingOptionKeyPressedReturnFalseWhenExit();

            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(c => c.ToString() == "Wrong input. Try again.")), Times.Exactly(2));
            Assert.That(result.Equals(true));
        }

        [Test]
        public void GenerateNewBoard_FromFileOptionChosen_CallGenerateFromFileWithCorrectStrategy()
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.Setup(_ => _.GetKeyCharToString(It.IsAny<ConsoleKeyInfo>())).Returns("1");

            game.LoopReadingOptionKeyPressedReturnFalseWhenExit();
            game.GenerateNewBoard(_fakeBoardGenerator.Object, _fakeFileWrapper.Object);

            _fakeBoardGenerator.Verify(_ => _.GenerateBoard(It.IsAny<BoardGenerationStrategyFromFile>()), Times.Once);
        }

        [Test]
        public void GenerateNewBoard_RandomOptionChosen_CallGenerateFromFileWithCorrectStrategy()
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.Setup(_ => _.GetKeyCharToString(It.IsAny<ConsoleKeyInfo>())).Returns("3");

            game.LoopReadingOptionKeyPressedReturnFalseWhenExit();
            game.GenerateNewBoard(_fakeBoardGenerator.Object, _fakeFileWrapper.Object);


            _fakeBoardGenerator.Verify(_ => _.GenerateBoard(It.IsAny<BoardGenerationStrategyRandom>()), Times.Once);
        }
    }
}
