using GameOfLife;
using GameOfLife.BoardGenerationStrategies;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BoardGeneratorTests
    {
        private Mock<IFile> _fakeFileWrapper;

        [SetUp]
        public void SetUp()
        {
            _fakeFileWrapper = new Mock<IFile>();
        }

        [Test]
        public void GenerateRandom_MethodCalled_BoardArrayPopulatedWithTrueOrFalseValues()
        {
            //Arrange
            var generator = new BoardGenerator();
            var strategy = new BoardGenerationStrategyRandom(1, 1);

            //Act
            var board = generator.GenerateBoard(strategy);

            //Assert
            Assert.That(board.BoardArray[0, 0].Equals(true) || board.BoardArray[0, 0].Equals(false));
        }

        [Test]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        public void GenerateRandom_GivenNumberOfRowsAndNumberOfColumns_BoardDimensionHaveCorrectLength(int noRows, int noColumns)
        {
            //Arrange
            var generator = new BoardGenerator();
            var strategy = new BoardGenerationStrategyRandom(noRows, noColumns);

            //Act
            var board = generator.GenerateBoard(strategy);

            //Assert
            Assert.That(board.LengthRows.Equals(noRows));
            Assert.That(board.LengthColumns.Equals(noColumns));
        }

        [Test]
        public void GenerateFromFile_GivenFileWithSingleAliveCharacter_BoardHasOneElementEqualToTrue()
        {
            //Arrange
            var generator = new BoardGenerator();
            var strategy = new BoardGenerationStrategyFromFile("C:\\a.txt", _fakeFileWrapper.Object);

            _fakeFileWrapper.Setup(_ => _.ReadAllText(It.IsAny<string>())).Returns($"{Constants.AliveChar}");

            //Act
            var board = generator.GenerateBoard(strategy);

            //Assert
            Assert.That(board.BoardArray[0, 0].Equals(true));
        }

        [Test]
        public void GenerateFromFile_GivenFileWithAliveAndDeadCharacter_BoardHasTrueAndFalse()
        {
            //Arrange
            var generator = new BoardGenerator();
            var strategy = new BoardGenerationStrategyFromFile("C:\\a.txt", _fakeFileWrapper.Object);

            _fakeFileWrapper.Setup(_ => _.ReadAllText(It.IsAny<string>())).Returns($"{Constants.AliveChar}\n{Constants.DeadChar}");

            //Act
            var board = generator.GenerateBoard(strategy);

            //Assert
            Assert.That(board.BoardArray[0, 0].Equals(true));
            Assert.That(board.BoardArray[1, 0].Equals(false));
        }
    }
}
