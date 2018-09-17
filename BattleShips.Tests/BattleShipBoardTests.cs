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

        [Theory]
        [InlineData(0, 0, 4, Direction.Horizontal, 0, 2, 3, Direction.Horizontal)]
        [InlineData(4, 2, 4, Direction.Horizontal, 4, 4, 3, Direction.Horizontal)]
        [InlineData(0, 0, 4, Direction.Vertical, 3, 0, 3, Direction.Vertical)]
        [InlineData(0, 4, 4, Direction.Vertical, 2, 3, 3, Direction.Vertical)]
        [InlineData(4, 0, 4, Direction.Horizontal, 3, 0, 3, Direction.Vertical)]
        [InlineData(0, 0, 4, Direction.Vertical, 3, 0, 3, Direction.Horizontal)]
        public void AddingShips_CanNotOverlapExistingShip(int row1, int col1, int length1, Direction direction1, int row2, int col2, int length2, Direction direction2)
        {
            var board = new BattleShipBoard(10,10);

            var mockShip1 = A.Fake<IShip>();
            A.CallTo(() => mockShip1.Length).Returns(length1);

            var firstShipStartPosition = new Position(col1, row1);
            board.Place(mockShip1, firstShipStartPosition, direction1);

            var mockShip2 = A.Fake<IShip>();
            A.CallTo(() => mockShip2.Length).Returns(length2);

            var secondShipStartPosition = new Position(col2, row2);

            Assert.Throws<OverlapException>(() => board.Place(mockShip2, secondShipStartPosition, direction2));

            Assert.Equal(1, board.Ships.Count);
        }


        [Theory]
        [InlineData(0, 0, 4, Direction.Horizontal, 0, 4, 3, Direction.Horizontal)]
        [InlineData(4, 2, 4, Direction.Horizontal, 4, 6, 3, Direction.Horizontal)]
        [InlineData(0, 0, 4, Direction.Vertical, 4, 0, 3, Direction.Vertical)]
        [InlineData(4, 0, 4, Direction.Horizontal, 0, 1, 3, Direction.Vertical)]
        [InlineData(0, 0, 4, Direction.Vertical, 3, 1, 3, Direction.Horizontal)]
        public void AddingShips_WithoutOverlap(int row1, int col1, int length1, Direction direction1, int row2, int col2, int length2, Direction direction2)
        {
            var board = new BattleShipBoard(10, 10);

            var mockShip1 = A.Fake<IShip>();
            A.CallTo(() => mockShip1.Length).Returns(length1);

            var firstShipStartPosition = new Position(col1, row1);
            board.Place(mockShip1, firstShipStartPosition, direction1);

            var mockShip2 = A.Fake<IShip>();
            A.CallTo(() => mockShip2.Length).Returns(length2);

            var secondShipStartPosition = new Position(col2, row2);

             board.Place(mockShip2, secondShipStartPosition, direction2);

            Assert.Equal(2, board.Ships.Count);
        }

    }
}
