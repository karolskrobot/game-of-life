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
        private Mock<IGame> _fakeGame;
        private Mock<IBoardProcessor> _fakeBoardProcessor;
        private Mock<IBoardGenerator> _fakeBoardGenerator;
        private Mock<IConsole> _fakeConsole;
        private Application _application;
        private Mock<IFile> _fakeFileWrapper;

        [SetUp]
        public void SetUp()
        {
            _fakeGame = new Mock<IGame>();
            _fakeBoardProcessor = new Mock<IBoardProcessor>();
            _fakeBoardGenerator = new Mock<IBoardGenerator>();
            _fakeConsole = new Mock<IConsole>();
            _fakeFileWrapper = new Mock<IFile>();
            _application = new Application(
                _fakeGame.Object, 
                _fakeBoardProcessor.Object, 
                _fakeBoardGenerator.Object, 
                _fakeConsole.Object,
                _fakeFileWrapper.Object
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

            _fakeGame.SetupSequence(g => g.SetOption())
                .Returns(true)
                .Returns(false);

            //Act
            _application.Run();

            //Assert                        
            _fakeGame.Verify(g => g.NewGame(), Times.Exactly(2));
            _fakeGame.Verify(g => g.NewBoard(_fakeBoardProcessor.Object, _fakeBoardGenerator.Object, _fakeFileWrapper.Object), Times.Once);
            _fakeBoardProcessor.Verify(b => b.EvolveBoard(), Times.Once);
            _fakeBoardProcessor.Verify(b => b.PrintBoard(), Times.Once);
        }

        [Test]
        public void Run_GameOptionFalse_NewGameOnceAndBoardNotSetOrRendered()
        {
            //Arrange
            _fakeGame.SetupSequence(g => g.SetOption())
                .Returns(false);

            //Act            
            _application.Run();

            //Assert
            _fakeGame.Verify(g => g.NewGame(), Times.Once);
            _fakeGame.Verify(g => g.NewBoard(_fakeBoardProcessor.Object, _fakeBoardGenerator.Object, _fakeFileWrapper.Object), Times.Never);
            _fakeBoardProcessor.Verify(b => b.EvolveBoard(), Times.Never);
            _fakeBoardProcessor.Verify(b => b.PrintBoard(), Times.Never);
        }
    }
}
