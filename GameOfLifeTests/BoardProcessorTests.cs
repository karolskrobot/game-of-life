using GameOfLife;
using GameOfLife.Wrappers;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BoardProcessorTests
    {
        private Mock<IConsole> _fakeConsole;

        [SetUp]
        public void SetUp()
        {
            _fakeConsole = new Mock<IConsole>();
        }

        [Test]
        public void Evolve_Once_ReturnCorrectOutput()
        {
            //Arrange
            var boardProcessor = new BoardProcessor(_fakeConsole.Object);
            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };
            var board = new Board
            {
                BoardArray = boardArray
            };
            boardProcessor.Board = board;

            //Act            
            boardProcessor.EvolveBoard();
            
            //Assert
            Assert.That(board.BoardArray[1, 1].Equals(false));
            Assert.That(board.BoardArray[1, 2].Equals(true));
            Assert.That(board.BoardArray[1, 3].Equals(false));
            Assert.That(board.BoardArray[2, 1].Equals(false));
            Assert.That(board.BoardArray[2, 2].Equals(true));
            Assert.That(board.BoardArray[2, 3].Equals(false));
            Assert.That(board.BoardArray[3, 1].Equals(false));
            Assert.That(board.BoardArray[3, 2].Equals(true));
            Assert.That(board.BoardArray[3, 3].Equals(false));            
        }

        [Test]
        public void Evolve_Twice_ReturnCorrectOutput()
        {          
            //Arrange

            var boardProcessor = new BoardProcessor(_fakeConsole.Object);
            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };
            var board = new Board()
            {
                BoardArray = boardArray
            };
            boardProcessor.Board = board;
            
            //Act
            boardProcessor.EvolveBoard();
            boardProcessor.EvolveBoard();
            
            //Assert
            Assert.That(board.BoardArray[1, 1].Equals(false));
            Assert.That(board.BoardArray[1, 2].Equals(false));
            Assert.That(board.BoardArray[1, 3].Equals(false));
            Assert.That(board.BoardArray[2, 1].Equals(true));
            Assert.That(board.BoardArray[2, 2].Equals(true));
            Assert.That(board.BoardArray[2, 3].Equals(true));
            Assert.That(board.BoardArray[3, 1].Equals(false));
            Assert.That(board.BoardArray[3, 2].Equals(false));
            Assert.That(board.BoardArray[3, 3].Equals(false));
        }

        [Test]
        public void Print_GivenBoolArray_PrintCorrectNumberOfCharacters()
        {
            //Arrange
            var boardProcessor = new BoardProcessor(_fakeConsole.Object);
            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };
            var board = new Board()
            {
                BoardArray = boardArray
            };

            //Act
            boardProcessor.Board = board;
            boardProcessor.PrintBoard();

            //Assert
            _fakeConsole.Verify(c => c.Clear(), Times.Once);
            _fakeConsole.Verify(c => c.Write(It.Is<string>(a => a.ToString() == Constants.AliveChar)), Times.Exactly(3));
            _fakeConsole.Verify(c => c.Write(It.Is<string>(a => a.ToString() == Constants.DeadChar)), Times.Exactly(22));
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == string.Empty)), Times.Exactly(7));
            _fakeConsole.Verify(c => c.WriteLine(It.Is<string>(a => a.ToString() == "Press ESC to return.")), Times.Once);
            _fakeConsole.VerifyNoOtherCalls();
        }
    }    
}
