using GameOfLife;
using GameOfLife.BoardArrayStrategies;
using GameOfLife.Core;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BoardFactory_Random_Tests
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 1)]
        [TestCase(1, 1)]
        [TestCase(1, 1)]
        [TestCase(1, 1)]
        public void CreateBoard_GivenBoardArrayStrategyRandom_BoardArrayPopulatedWithTrueOrFalseValues(int noOfRows, int noOfColumns)
        {
            //Arrange
            var factory = new BoardFactory();
            var strategy = new BoardArrayStrategyRandom(noOfRows, noOfColumns);

            //Act
            var board = factory.CreateBoard(strategy);

            //Assert
            Assert.That(board.GetTileValue(0, 0).Equals(true) || board.GetTileValue(0, 0).Equals(false));
        }

        [Test]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        public void CreateBoard_GivenBoardArrayStrategyRandom_BoardDimensionHaveCorrectLength(int noOfRows, int noOfColumns)
        {
            //Arrange
            var factory = new BoardFactory();
            var strategy = new BoardArrayStrategyRandom(noOfRows, noOfColumns);

            //Act
            var board = factory.CreateBoard(strategy);

            //Assert
            Assert.That(board.LengthRows.Equals(noOfRows));
            Assert.That(board.LengthColumns.Equals(noOfColumns));
        }
    }
}