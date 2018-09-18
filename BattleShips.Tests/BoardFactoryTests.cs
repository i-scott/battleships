using System;
using FakeItEasy;
using Xunit;

namespace BattleShips.Tests
{
    public class BoardFactoryTests
    {
        private const int DefaultWidth = 10;
        private const int DefaultHeight = 10;

        [Fact]
        public void Create_ReturnsBattleshipBoard()
        {
            var boardFactory = new BoardFactory(A.Fake<IShipBuilder>());
            
            var board = boardFactory.Create();

            Assert.IsAssignableFrom<IBoard>(board);
        }

        [Fact]
        public void Create_CreatesBoardOfDefaultSize_10x10()
        {
            var boardFactory = new BoardFactory(A.Fake<IShipBuilder>());

            var board = boardFactory.Create();

            Assert.Equal(DefaultHeight, board.Height);
            Assert.Equal(DefaultWidth, board.Width);
        }

        [Theory]
        [InlineData(12, 12)]
        [InlineData(20, 20)]
        public void Create_CreatesBoardOfRequestedSize(int requestedSize, int expectedSize)
        {
            var boardFactory = new BoardFactory(A.Fake<IShipBuilder>());

            var board = boardFactory.Create(requestedSize);

            Assert.Equal(expectedSize, board.Height);
            Assert.Equal(expectedSize, board.Width);
        }


    }
}
