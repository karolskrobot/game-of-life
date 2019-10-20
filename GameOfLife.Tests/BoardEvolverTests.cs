using GameOfLife;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class BoardEvolverTests
    {
        [Test]
        public void Evolve_Once_ReturnCorrectOutput()
        {
            //Arrange
            var boardEvolver = new BoardEvolver();

            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, true , false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };

            var evolvedArray = new[,] {
                {false, false, false, false, false},
                {false, true , false, false, false},
                {false, true , true , false, false},
                {false, false, true , false, false},
                {false, false, false, false, false}
            };

            var board = new Board
            {
                BoardArray = boardArray
            };
            
            //Act            
            boardEvolver.EvolveBoard(board);

            //Assert
            Assert.AreEqual(evolvedArray, board.BoardArray);
        }

        [Test]
        public void Evolve_Twice_ReturnCorrectOutput()
        {          
            //Arrange

            var boardEvolver = new BoardEvolver();

            var boardArray = new[,] {
                {false, false, false, false, false},
                {false, true , false, false, false},
                {false, true , true , true , false},
                {false, false, false, false, false},
                {false, false, false, false, false}
            };

            var evolvedArray = new[,] {
                {false, false, false, false, false},
                {false, true , true , false, false},
                {false, true , true , false, false},
                {false, true , true , false, false},
                {false, false, false, false, false}
            };

            var board = new Board()
            {
                BoardArray = boardArray
            };
            
            //Act
            boardEvolver.EvolveBoard(board);
            boardEvolver.EvolveBoard(board);

            //Assert
            Assert.AreEqual(evolvedArray, board.BoardArray);
        }
    }    
}
