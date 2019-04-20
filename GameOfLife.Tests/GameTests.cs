using GameOfLife;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class GameTests
    {        
        private Mock<IDirectory> _fakeDirectoryWrapper;
        private Mock<IConsole> _fakeConsole;
        private Mock<IBoardProcessor> _fakeBoardProcessor;
        private Mock<IBoardGenerator> _fakeBoardGenerator;
        private Mock<IFile> _fakeFileWrapper;

        [SetUp]
        public void SetUp()
        {
            _fakeDirectoryWrapper = new Mock<IDirectory>();
            _fakeConsole = new Mock<IConsole>();
            _fakeBoardProcessor = new Mock<IBoardProcessor>();
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
            game.NewGame();

            //Assert
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "1: A")), Times.Once);
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "2: B")), Times.Once);
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "3: Random")), Times.Once);
        }

        [Test]
        public void SetOption_ExitCharacterPressed_ReturnsFalse()
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.Setup(_ => _.ReadLine()).Returns("X");

            var result = game.SetOption();

            Assert.That(result.Equals(false));
        }

        [Test]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        public void SetOption_BoardChoiceCharacterPressed_ReturnsTrue(string boardChoice)
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.Setup(_ => _.ReadLine()).Returns(boardChoice);

            var result = game.SetOption();

            Assert.That(result.Equals(true));
        }

        [Test]
        public void SetOption_2IncorrectInputsUntilCorrect_WritesErrorMessage2TimesThenReturnsTrue()
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.SetupSequence(_ => _.ReadLine())
                .Returns("A")
                .Returns("4")
                .Returns("1");

            var result = game.SetOption();

            _fakeConsole.Verify(_ => _.WriteLine(It.Is<string>(c => c.ToString() == "Wrong input. Try again.")), Times.Exactly(2));
            Assert.That(result.Equals(true));
        }

        [Test]
        public void NewBoard_FromFileOptionChosen_CallGenerateFromFileWithCorrectFileNameArgument()
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.Setup(_ => _.ReadLine()).Returns("1");

            game.SetOption();
            game.NewBoard(_fakeBoardProcessor.Object, _fakeBoardGenerator.Object, _fakeFileWrapper.Object);

            _fakeBoardGenerator
                .Verify(_ => _.GenerateFromFile(It.Is<string>(c => c.ToString() == "a.txt"), _fakeFileWrapper.Object), Times.Once);
        }

        [Test]        
        public void NewBoard_RandomOptionChosen_CallGenerateRandomWithConstantArguments()
        {
            var game = new Game(_fakeConsole.Object, _fakeDirectoryWrapper.Object);
            _fakeConsole.Setup(_ => _.ReadLine()).Returns("3");

            game.SetOption();
            game.NewBoard(_fakeBoardProcessor.Object, _fakeBoardGenerator.Object, _fakeFileWrapper.Object);

            _fakeBoardGenerator.Verify(_ => _.GenerateRandom(Constants.BoardRows, Constants.BoardColumns), Times.Once);
        }
    }
}
