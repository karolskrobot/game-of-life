using GameOfLife;
using GameOfLife.Core;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void Evolve_Once_ReturnCorrectOutput()
        {
            //Arrange
            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, true , false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };

            var board = new Board(boardArray);

            //evolved board should be:
            //{false, false, false, false, false},
            //{false, true , false, false, false},
            //{false, true , true , false, false},
            //{false, false, true , false, false},
            //{false, false, false, false, false}

            //Act            
            board.Evolve();
            
            //Assert
            Assert.AreEqual(board.GetTileValue(0, 0), false);
            Assert.AreEqual(board.GetTileValue(0, 1), false);
            Assert.AreEqual(board.GetTileValue(0, 2), false);
            Assert.AreEqual(board.GetTileValue(0, 3), false);
            Assert.AreEqual(board.GetTileValue(0, 4), false);
            Assert.AreEqual(board.GetTileValue(1, 0), false);
            Assert.AreEqual(board.GetTileValue(1, 1), true);
            Assert.AreEqual(board.GetTileValue(1, 2), false);
            Assert.AreEqual(board.GetTileValue(1, 3), false);
            Assert.AreEqual(board.GetTileValue(1, 4), false);
            Assert.AreEqual(board.GetTileValue(2, 0), false);
            Assert.AreEqual(board.GetTileValue(2, 1), true);
            Assert.AreEqual(board.GetTileValue(2, 2), true);
            Assert.AreEqual(board.GetTileValue(2, 3), false);
            Assert.AreEqual(board.GetTileValue(2, 4), false);
            Assert.AreEqual(board.GetTileValue(3, 0), false);
            Assert.AreEqual(board.GetTileValue(3, 1), false);
            Assert.AreEqual(board.GetTileValue(3, 2), true);
            Assert.AreEqual(board.GetTileValue(3, 3), false);
            Assert.AreEqual(board.GetTileValue(3, 4), false);
            Assert.AreEqual(board.GetTileValue(4, 0), false);
            Assert.AreEqual(board.GetTileValue(4, 1), false);
            Assert.AreEqual(board.GetTileValue(4, 2), false);
            Assert.AreEqual(board.GetTileValue(4, 3), false);
            Assert.AreEqual(board.GetTileValue(4, 4), false);

        }

        [Test]
        public void Evolve_Twice_ReturnCorrectOutput()
        {
            //Arrange
            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, true , false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };
            
            var board = new Board(boardArray);

            //evolved board should be:
            //{ false, false, false, false, false},
            //{ false, true , true , false, false},
            //{ false, true , true , false, false},
            //{ false, true , true , false, false},
            //{ false, false, false, false, false}

            //Act
            board.Evolve();
            board.Evolve();

            //Assert
            Assert.AreEqual(board.GetTileValue(0, 0), false);
            Assert.AreEqual(board.GetTileValue(0, 1), false);
            Assert.AreEqual(board.GetTileValue(0, 2), false);
            Assert.AreEqual(board.GetTileValue(0, 3), false);
            Assert.AreEqual(board.GetTileValue(0, 4), false);
            Assert.AreEqual(board.GetTileValue(1, 0), false);
            Assert.AreEqual(board.GetTileValue(1, 1), true);
            Assert.AreEqual(board.GetTileValue(1, 2), true);
            Assert.AreEqual(board.GetTileValue(1, 3), false);
            Assert.AreEqual(board.GetTileValue(1, 4), false);
            Assert.AreEqual(board.GetTileValue(2, 0), false);
            Assert.AreEqual(board.GetTileValue(2, 1), true);
            Assert.AreEqual(board.GetTileValue(2, 2), true);
            Assert.AreEqual(board.GetTileValue(2, 3), false);
            Assert.AreEqual(board.GetTileValue(2, 4), false);
            Assert.AreEqual(board.GetTileValue(3, 0), false);
            Assert.AreEqual(board.GetTileValue(3, 1), true);
            Assert.AreEqual(board.GetTileValue(3, 2), true);
            Assert.AreEqual(board.GetTileValue(3, 3), false);
            Assert.AreEqual(board.GetTileValue(3, 4), false);
            Assert.AreEqual(board.GetTileValue(4, 0), false);
            Assert.AreEqual(board.GetTileValue(4, 1), false);
            Assert.AreEqual(board.GetTileValue(4, 2), false);
            Assert.AreEqual(board.GetTileValue(4, 3), false);
            Assert.AreEqual(board.GetTileValue(4, 4), false);
        }
    }    
}
