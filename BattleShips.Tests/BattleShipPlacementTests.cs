using BattleShips.Ships;
using FakeItEasy;
using Xunit;

namespace BattleShips.Tests
{
    public class BattleShipPlacementTests
    {
        private IShipBuilder fakeShipYard;

        public BattleShipPlacementTests()
        {
            fakeShipYard = A.Fake<IShipBuilder>();
            A.CallTo(() => fakeShipYard.Build(A<ShipType>.Ignored, A<Position>.Ignored, A<Direction>.Ignored))
                .WithAnyArguments().ReturnsLazily(
                    (ShipType typeOfShip, Position position, Direction direction) => new Ship(typeOfShip, position, direction));
        }

        [Fact]
        public void PalceShipHorizonatal_AddsShipToBoard()
        {
            var board = new BattleShipBoard(10,10, fakeShipYard);

            var startPosition = new Position(0, 0);
            board.Place(ShipType.Frigate, startPosition, Direction.Horizontal);

            Assert.Equal(1, board.Ships.Count);
        }

        [Fact]
        public void PalceShipVertically_AddsShipToBoard()
        {
            var board = new BattleShipBoard(10, 10, fakeShipYard);

            var startPosition = new Position(0, 0);
            board.Place(ShipType.Frigate, startPosition, Direction.Vertical);

            Assert.Equal(1, board.Ships.Count);
        }

        [Fact]
        public void PlaceHorizontally_Throws_ExceptionWhenOutOfBounds()
        {
            var board = new BattleShipBoard(10, 10, fakeShipYard);

            var startPosition = new Position( 8, 0);

            Assert.Throws<OutOfBoundsException>(() => board.Place(ShipType.Frigate, startPosition, Direction.Horizontal));

            Assert.Equal(0, board.Ships.Count);
        }

        [Fact]
        public void PlaceVertically_Throws_ExceptionWhenOutOfBounds()
        {
            var board = new BattleShipBoard(10, 10, fakeShipYard);

            var startPosition = new Position(0, 8);

            Assert.Throws<OutOfBoundsException>(() => board.Place(ShipType.Frigate, startPosition, Direction.Vertical));

            Assert.Equal(0, board.Ships.Count);
        }


        [Fact]
        public void AddingShipsHorizontally_ThrowsOverlapExcpetionIfExistingShipInSpace()
        {
            var board = new BattleShipBoard(10, 10, fakeShipYard);

            var firstShipStartPosition = new Position(0, 0);
            board.Place(ShipType.Frigate, firstShipStartPosition, Direction.Horizontal);

            var secondShipStartPosition = new Position(2, 0);

            Assert.Throws<OverlapException>(() => board.Place(ShipType.Frigate, secondShipStartPosition, Direction.Horizontal));

            Assert.Equal(1, board.Ships.Count);
        }

        [Fact]
        public void AddingShipsVertically_ThrowsOverlapExcpetionIfExistingShipInSpace()
        {
            var board = new BattleShipBoard(10, 10, fakeShipYard);

            var firstShipStartPosition = new Position(0, 0);
            board.Place(ShipType.Frigate, firstShipStartPosition, Direction.Vertical);

            var secondShipStartPosition = new Position(0, 2);

            Assert.Throws<OverlapException>(() => board.Place(ShipType.Frigate, secondShipStartPosition, Direction.Vertical));

            Assert.Equal(1, board.Ships.Count);
        }

        [Theory]
        [InlineData(0, 0, ShipType.Frigate, Direction.Horizontal, 0, 2, ShipType.Sub, Direction.Horizontal)]
        [InlineData(4, 2, ShipType.Frigate, Direction.Horizontal, 4, 4, ShipType.Sub, Direction.Horizontal)]
        [InlineData(0, 0, ShipType.Frigate, Direction.Vertical, 3, 0, ShipType.Sub, Direction.Vertical)]
        [InlineData(0, 4, ShipType.Frigate, Direction.Vertical, 2, 3, ShipType.Sub, Direction.Vertical)]
        [InlineData(4, 0, ShipType.Frigate, Direction.Horizontal, 3, 0, ShipType.Sub, Direction.Vertical)]
        [InlineData(0, 0, ShipType.Frigate, Direction.Vertical, 3, 0, ShipType.Sub, Direction.Horizontal)]
        public void AddingShips_CanNotOverlapExistingShip(int row1, int col1, ShipType typeOfShip1, Direction direction1, int row2, int col2, ShipType typeOfShip2, Direction direction2)
        {
            var board = new BattleShipBoard(10,10, fakeShipYard);

            var firstShipStartPosition = new Position(col1, row1);
            board.Place(typeOfShip1, firstShipStartPosition, direction1);

            var secondShipStartPosition = new Position(col2, row2);
            Assert.Throws<OverlapException>(() => board.Place(typeOfShip2, secondShipStartPosition, direction2));

            Assert.Equal(1, board.Ships.Count);
        }


        [Theory]
        [InlineData(0, 0, ShipType.Frigate, Direction.Horizontal, 0, 4, ShipType.Sub, Direction.Horizontal)]
        [InlineData(4, 2, ShipType.Frigate, Direction.Horizontal, 4, 6, ShipType.Sub, Direction.Horizontal)]
        [InlineData(0, 0, ShipType.Frigate, Direction.Vertical, 4, 0, ShipType.Sub, Direction.Vertical)]
        [InlineData(4, 0, ShipType.Frigate, Direction.Horizontal, 0, 1, ShipType.Sub, Direction.Vertical)]
        [InlineData(0, 0, ShipType.Frigate, Direction.Vertical, 3, 1, ShipType.Sub, Direction.Horizontal)]
        public void AddingShips_WithoutOverlap(int row1, int col1, ShipType typeOfShip1, Direction direction1, int row2, int col2, ShipType typeOfShip2, Direction direction2)
        {
            var board = new BattleShipBoard(10, 10, fakeShipYard);

            var firstShipStartPosition = new Position(col1, row1);
            board.Place(typeOfShip1, firstShipStartPosition, direction1);

            var secondShipStartPosition = new Position(col2, row2);
            board.Place(typeOfShip2, secondShipStartPosition, direction2);


            Assert.Equal(2, board.Ships.Count);
        }

    }
}
