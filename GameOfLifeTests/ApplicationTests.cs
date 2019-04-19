using GameOfLife;
using Moq;
using NUnit.Framework;
using System;

namespace GameOfLifeTests
{
    [TestFixture]
    public class ApplicationTests
    {
        private Mock<IGame> _fakeGame;
        private Mock<IBoard> _fakeBoard;
        private Mock<IBoardGenerator> _fakeBoardGenerator;
        private Mock<IConsole> _fakeConsole;
        private Application _application;

        [SetUp]
        public void SetUp()
        {
            _fakeGame = new Mock<IGame>();
            _fakeBoard = new Mock<IBoard>();
            _fakeBoardGenerator = new Mock<IBoardGenerator>();
            _fakeConsole = new Mock<IConsole>();
            _application = new Application(_fakeGame.Object, _fakeBoard.Object, _fakeBoardGenerator.Object, _fakeConsole.Object);
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
            _fakeGame.Verify(g => g.SetBoard(_fakeBoard.Object, _fakeBoardGenerator.Object), Times.Once);
            _fakeBoard.Verify(b => b.Evolve(), Times.Once);
            _fakeBoard.Verify(b => b.Print(), Times.Once);
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
            _fakeGame.Verify(g => g.SetBoard(_fakeBoard.Object, _fakeBoardGenerator.Object), Times.Never);
            _fakeBoard.Verify(b => b.Evolve(), Times.Never);
            _fakeBoard.Verify(b => b.Print(), Times.Never);
        }
    }
}
