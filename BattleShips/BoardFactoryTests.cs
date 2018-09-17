using System;
using Xunit;

namespace BattleShips
{
    public class BoardFactoryTests
    {
        [Fact]
        public void Create_ReturnsBattleshipBoard()
        {
            var boardFactory = new BoardFactory();
            
            var board = boardFactory.Create();

            Assert.IsAssignableFrom<IBoard>(board);
        }

    }

    public class BoardFactory
    {
       
        public IBoard Create()
        {
            return new BattleShipBoard();
        }
    }

    public interface IBoard
    {
        
    }

    public class BattleShipBoard : IBoard
    {

    }
}
