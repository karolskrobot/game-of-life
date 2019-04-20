using GameOfLife;
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
            var board = new Board();
            var generator = new BoardGenerator(board);

            //Act
            generator.GenerateRandom(1, 1);

            //Assert
            Assert.That(board.BoardArray[0,0].Equals(true) || board.BoardArray[0,0].Equals(false));
        }

        [Test]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        public void GenerateRandom_GivenNumberOfRowsAndNumberOfColumns_BoardDimensionHaveCorrectLength(int noRows, int noColumns)
        {
            //Arrange
            var board = new Board();
            var generator = new BoardGenerator(board);
            
            //Act
            generator.GenerateRandom(noRows, noColumns);

            //Assert
            Assert.That(board.LengthX.Equals(noRows));
            Assert.That(board.LengthY.Equals(noColumns));
        }

        [Test]
        public void GenerateFromFile_GivenFileWithSingleAliveCharacter_BoardHasOneElementEqualToTrue()
        {
            //Arrange
            var board = new Board();
            var generator = new BoardGenerator(board);

            _fakeFileWrapper.Setup(_ => _.ReadAllText(It.IsAny<string>())).Returns($"{Constants.AliveChar}");

            //Act
            generator.GenerateFromFile("C:\\a.txt", _fakeFileWrapper.Object);

            //Assert
            Assert.That(board.BoardArray[0,0].Equals(true));            
        }

        [Test]
        public void GenerateFromFile_GivenFileWithAliveAndDeadCharacter_BoardHasTrueAndFalse()
        {
            //Arrange
            var board = new Board();
            var generator = new BoardGenerator(board);

            _fakeFileWrapper.Setup(_ => _.ReadAllText(It.IsAny<string>())).Returns($"{Constants.AliveChar}\n{Constants.DeadChar}");

            //Act
            generator.GenerateFromFile("C:\\a.txt", _fakeFileWrapper.Object);

            //Assert
            Assert.That(board.BoardArray[0,0].Equals(true));
            Assert.That(board.BoardArray[1,0].Equals(false));
        }
    }
}
