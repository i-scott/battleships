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
}
