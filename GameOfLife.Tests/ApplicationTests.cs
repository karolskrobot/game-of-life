using GameOfLife;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;
using System;

namespace GameOfLifeTests
{
    [TestFixture]
    public class ApplicationTests
    {
        private Mock<IConsole> _fakeConsole;
        private Mock<IFile> _fakeFileWrapper;
        private Mock<IGame> _fakeGame;
        private Mock<IBoardEvolver> _fakeBoardEvolver;
        private Mock<IBoardGenerator> _fakeBoardGenerator;
        private Mock<IBoardPrinter> _fakeBoardPrinter;
        private Application _application;

        [SetUp]
        public void SetUp()
        {
            _fakeGame = new Mock<IGame>();
            _fakeBoardEvolver = new Mock<IBoardEvolver>();
            _fakeBoardGenerator = new Mock<IBoardGenerator>();
            _fakeConsole = new Mock<IConsole>();
            _fakeFileWrapper = new Mock<IFile>();
            _fakeBoardPrinter = new Mock<IBoardPrinter>();

            _application = new Application(
                _fakeConsole.Object,
                _fakeFileWrapper.Object,
                _fakeGame.Object,
                _fakeBoardEvolver.Object,
                _fakeBoardGenerator.Object,
                _fakeBoardPrinter.Object
            );
        }

        [Test]
        public void Run_GameOptionTrue_NewGameTwiceAndBoardSetAndRendered()
        {
            //Arrange
            _fakeConsole.SetupSequence(c => c.ReadKey(It.IsAny<bool>()))
                .Returns(ConsoleKey.Escape);

            _fakeConsole.SetupSequence(c => c.KeyAvailable)
                .Returns(false)
                .Returns(true);

            _fakeGame.SetupSequence(g => g.LoopReadingOptionKeyPressedReturnFalseWhenExit())
                .Returns(true)
                .Returns(false);

            //Act
            _application.Run();

            //Assert                        
            _fakeGame.Verify(g => g.PrintNewGameScreen(), Times.Exactly(2));
            _fakeGame.Verify(g => g.GenerateNewBoard(_fakeBoardGenerator.Object, _fakeFileWrapper.Object), Times.Once);
            _fakeBoardEvolver.Verify(b => b.EvolveBoard(It.IsAny<IBoard>()), Times.Once);
            _fakeBoardPrinter.Verify(b => b.PrintBoard(It.IsAny<IBoard>()), Times.Exactly(2));
        }

        [Test]
        public void Run_GameOptionFalse_NewGameOnceAndBoardNotSetOrRendered()
        {
            //Arrange
            _fakeGame.SetupSequence(g => g.LoopReadingOptionKeyPressedReturnFalseWhenExit())
                .Returns(false);

            //Act            
            _application.Run();

            //Assert
            _fakeGame.Verify(g => g.PrintNewGameScreen(), Times.Once);
            _fakeGame.Verify(g => g.GenerateNewBoard(_fakeBoardGenerator.Object, _fakeFileWrapper.Object), Times.Never);
            _fakeBoardEvolver.Verify(b => b.EvolveBoard(It.IsAny<IBoard>()), Times.Never);
            _fakeBoardPrinter.Verify(b => b.PrintBoard(It.IsAny<IBoard>()), Times.Never);
        }
    }
}
