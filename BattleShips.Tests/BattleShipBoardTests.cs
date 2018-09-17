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


        [Fact]
        public void AddingShipsHorizontally_ThrowsOverlapExcpetionIfExistingShipInSpace()
        {
            var board = new BattleShipBoard(10,10);

            var mockShip = A.Fake<IShip>();
            A.CallTo(() => mockShip.Length).Returns(4);

            var firstShipStartPosition = new Position(0, 0);
            board.Place(mockShip, firstShipStartPosition, Direction.Horizontal);

            var secondShipStartPosition = new Position(2, 0);

            Assert.Throws<OverlapException>(() => board.Place(mockShip, secondShipStartPosition, Direction.Horizontal));

            Assert.Equal(1, board.Ships.Count);
        }

        [Fact]
        public void AddingShipsVertically_ThrowsOverlapExcpetionIfExistingShipInSpace()
        {
            var board = new BattleShipBoard(10, 10);

            var mockShip = A.Fake<IShip>();
            A.CallTo(() => mockShip.Length).Returns(4);

            var firstShipStartPosition = new Position(0, 0);
            board.Place(mockShip, firstShipStartPosition, Direction.Vertical);

            var secondShipStartPosition = new Position(0, 2);

            Assert.Throws<OverlapException>(() => board.Place(mockShip, secondShipStartPosition, Direction.Vertical));

            Assert.Equal(1, board.Ships.Count);
        }

    }
}
