using GameOfLife.BoardArrayStrategies;
using GameOfLife.Core;
using GameOfLife.IO;
using GameOfLife.IO.Wrappers;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BoardFactory_FromFile_Tests
    {
        private Mock<IFile> _fakeFileWrapper;

        [SetUp]
        public void SetUp()
        {
            _fakeFileWrapper = new Mock<IFile>();
        }

        [Test]
        public void CreateBoard_GivenBoardArrayStrategyFromFileSingleAliveCharacter_ReturnBoardWithCorrectElements()
        {
            //Arrange
            var factory = new BoardFactory();
            var strategy = new BoardArrayStrategyFromFile("C:\\a.txt", _fakeFileWrapper.Object);

            _fakeFileWrapper.Setup(_ => _.ReadAllText(It.IsAny<string>())).Returns($"{Constants.AliveChar}");

            //Act
            var board = factory.CreateBoard(strategy);

            //Assert
            Assert.That(board.GetTileValue(0, 0).Equals(true));
            Assert.That(board.LengthRows == 1);
            Assert.That(board.LengthColumns == 1);
        }

        [Test]
        public void CreateBoard_GivenBoardArrayStrategyFromFileAliveAndDeadCharacter_BoardHasTrueAndFalse()
        {
            //Arrange
            var factory = new BoardFactory();
            var strategy = new BoardArrayStrategyFromFile("C:\\a.txt", _fakeFileWrapper.Object);

            _fakeFileWrapper.Setup(_ => _.ReadAllText(It.IsAny<string>())).Returns($"{Constants.AliveChar}\n{Constants.DeadChar}");

            //Act
            var board = factory.CreateBoard(strategy);

            //Assert
            Assert.That(board.GetTileValue(0, 0).Equals(true));
            Assert.That(board.GetTileValue(1, 0).Equals(false));
            Assert.That(board.LengthRows == 2);
            Assert.That(board.LengthColumns == 1);
        }
    }
}