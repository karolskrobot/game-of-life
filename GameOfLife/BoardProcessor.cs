using System.Collections.Generic;
using System.Linq;
using GameOfLife.Wrappers;

namespace GameOfLife
{
    public class BoardProcessor : IBoardProcessor
    {        
        public IBoard Board { get; set; }

        private IConsole _console;
        
        public BoardProcessor(IConsole console)
        {            
            _console = console;
        }

        private IEnumerable<bool> GetTileNeighbours(int boardTileY, int boardTileX)
        {
            var neighbours = new List<bool>();

            for (var i = boardTileY - 1; i <= boardTileY + 1; i++)
            {
                for (var j = boardTileX - 1; j <= boardTileX + 1; j++)
                {
                    if (i == boardTileY && j == boardTileX)
                    {
                        continue;
                    }
                    //if neighbour out of bounds add as dead
                    else if (i >= Board.LengthX || i < 0 || j >= Board.LengthY || j < 0)
                    {
                        neighbours.Add(false);
                    }
                    else
                    {
                        neighbours.Add(Board.BoardArray[i, j]);
                    }
                }
            }

            return neighbours;
        }

        public void EvolveBoard()
        {
            var boardAfter = new bool[Board.LengthX, Board.LengthY];

            for (var i = 0; i < Board.LengthX; i++)
            {
                for (var j = 0; j < Board.LengthY; j++)
                {
                    var aliveCounter = GetTileNeighbours(i, j).Count(n => n);

                    switch (Board.BoardArray[i, j])
                    {
                        // if dead tile has exactly 3 neighbours that are alive it comes to life
                        case false when aliveCounter == 3:
                            boardAfter[i, j] = true;
                            break;

                        // if alive tile has 0 or 1 neighbours (is lonely) or more than 3 neighbours (overcrowded) it dies
                        case true when (aliveCounter < 2 || aliveCounter > 3):
                            boardAfter[i, j] = false;
                            break;

                        default:
                            boardAfter[i, j] = Board.BoardArray[i, j];
                            break;
                    }
                }
            }

            Board.BoardArray = boardAfter;
        }

        public void PrintBoard()
        {
            _console.Clear();
            _console.WriteLine(string.Empty);

            for (var i = 0; i < Board.LengthX; i++)
            {
                for (var j = 0; j < Board.LengthY; j++)
                    _console.Write(Board.BoardArray[i, j] ? Constants.AliveChar : Constants.DeadChar);

                _console.WriteLine(string.Empty);
            }

            _console.WriteLine(string.Empty);
            _console.WriteLine("Press ESC to return.");
        }
    }
}
