using FakeItEasy;
using Xunit;

namespace BattleShips.Tests
{
    public class BattleShipBoardTests
    {

        [Fact]
        public void PalceShipHorizonatal_AddsShipToBoard()
        {
            var board = new BattleShipBoard(10,10);

            var mockShip = A.Fake<IShip>();
            A.CallTo(() => mockShip.Length).Returns(4);

            var startPosition = new Position(0, 0);
            board.Place(mockShip, startPosition, Direction.Horizontal);

            Assert.Equal(1, board.Ships.Count);
        }

        [Fact]
        public void PalceShipVertically_AddsShipToBoard()
        {
            var board = new BattleShipBoard(10, 10);

            var mockShip = A.Fake<IShip>();
            A.CallTo(() => mockShip.Length).Returns(4);

            var startPosition = new Position(0, 0);
            board.Place(mockShip, startPosition, Direction.Vertical);

            Assert.Equal(1, board.Ships.Count);
        }

        [Fact]
        public void PlaceHorizontally_Throws_ExceptionWhenOutOfBounds()
        {
            var board = new BattleShipBoard(10, 10);

            var mockShip = A.Fake<IShip>();
            A.CallTo(() => mockShip.Length).Returns(4);

            var startPosition = new Position( 8, 0);

            Assert.Throws<OutOfBoundsException>(() => board.Place(mockShip, startPosition, Direction.Horizontal));

            Assert.Equal(0, board.Ships.Count);
        }

        [Fact]
        public void PlaceVertically_Throws_ExceptionWhenOutOfBounds()
        {
            var board = new BattleShipBoard(10, 10);

            var mockShip = A.Fake<IShip>();
            A.CallTo(() => mockShip.Length).Returns(4);

            var startPosition = new Position(0, 8);

            Assert.Throws<OutOfBoundsException>(() => board.Place(mockShip, startPosition, Direction.Vertical));

            Assert.Equal(0, board.Ships.Count);
        }

    }
}
