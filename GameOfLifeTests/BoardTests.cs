using GameOfLife;
using Moq;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BoardTests
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
            var board = new Board(_fakeConsole.Object);

            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };

            //Act
            board.Set(boardArray);
            board.Evolve();
            
            //Assert
            Assert.That(boardArray[1, 1].Equals(false));
            Assert.That(boardArray[1, 2].Equals(true));
            Assert.That(boardArray[1, 3].Equals(false));
            Assert.That(boardArray[2, 1].Equals(false));
            Assert.That(boardArray[2, 2].Equals(true));
            Assert.That(boardArray[2, 3].Equals(false));
            Assert.That(boardArray[3, 1].Equals(false));
            Assert.That(boardArray[3, 2].Equals(true));
            Assert.That(boardArray[3, 3].Equals(false));            
        }

        [Test]
        public void Evolve_Twice_ReturnCorrectOutput()
        {          
            //Arrange
            var board = new Board(_fakeConsole.Object);
            
            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };
            
            //Act
            board.Set(boardArray);
            board.Evolve();
            board.Evolve();
            
            //Assert
            Assert.That(boardArray[1, 1].Equals(false));
            Assert.That(boardArray[1, 2].Equals(false));
            Assert.That(boardArray[1, 3].Equals(false));
            Assert.That(boardArray[2, 1].Equals(true));
            Assert.That(boardArray[2, 2].Equals(true));
            Assert.That(boardArray[2, 3].Equals(true));
            Assert.That(boardArray[3, 1].Equals(false));
            Assert.That(boardArray[3, 2].Equals(false));
            Assert.That(boardArray[3, 3].Equals(false));
        }

        [Test]
        public void Print_GivenBoolArray_PrintCorrectNumberOfCharacters()
        {
            //Arrange
            var board = new Board(_fakeConsole.Object);

            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };

            //Act
            board.Set(boardArray);
            board.Print();

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
