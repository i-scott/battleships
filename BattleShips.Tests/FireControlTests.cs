using BattleShips.Ships;
using FakeItEasy;
using Xunit;

namespace BattleShips.Tests
{
    public class FireControlTests
    {
        private readonly IShipBuilder _fakeShipYard;

        protected IBoard board;
        public FireControlTests()
        {
            _fakeShipYard = A.Fake<IShipBuilder>();
            A.CallTo(() => _fakeShipYard.Build(A<ShipType>.Ignored, A<Position>.Ignored, A<Direction>.Ignored))
                .WithAnyArguments().ReturnsLazily(
                    (ShipType typeOfShip, Position position, Direction direction) => new Ship(typeOfShip, position, direction));
        }

        [Fact]
        public void WhenFiredOn_ReportsHit()
        {
            board = new BattleShipBoard(10, 10, _fakeShipYard);

            board.Place(ShipType.Frigate, new Position(0, 0), Direction.Horizontal);

            var position = new Position(0,0);
            var hitResult = board.FireOn(position);

            Assert.Equal(HitReult.Hit, hitResult);
        }

        [Fact]
        public void WhenFiredOn_ReportsMiss()
        {
            board = new BattleShipBoard(10, 10, _fakeShipYard);

            board.Place(ShipType.Frigate, new Position(0, 0), Direction.Horizontal);

            var position = new Position(4, 0);
            var hitResult = board.FireOn(position);

            Assert.Equal(HitReult.Miss, hitResult);
        }

        [Fact]
        public void WhenFiredOn_ReportsSunk()
        {
            board = new BattleShipBoard(10, 10, _fakeShipYard);

            board.Place(ShipType.Frigate, new Position(0, 0), Direction.Horizontal);
            board.Place(ShipType.PatrolBoat, new Position(0, 1), Direction.Horizontal);

            board.FireOn(new Position(0,0));
            board.FireOn(new Position(1, 0));
            board.FireOn(new Position(2, 0));
            var hitResult = board.FireOn(new Position(3, 0));

            Assert.Equal(HitReult.Sunk, hitResult);
        }

        [Fact]
        public void WhenAllShipsSunk_ReportsLost()
        {
            board = new BattleShipBoard(10, 10, _fakeShipYard);

            board.Place(ShipType.Frigate, new Position(0, 0), Direction.Horizontal);
            board.Place(ShipType.PatrolBoat, new Position(0, 1), Direction.Horizontal);

            board.FireOn(new Position(0, 0));
            board.FireOn(new Position(1, 0));
            board.FireOn(new Position(2, 0));
            board.FireOn(new Position(3, 0));

            var hitResult = board.FireOn(new Position(0, 1));

            Assert.Equal(HitReult.Lost, hitResult);
        }
    }
}
