using BattleShips.Ships;
using FakeItEasy;
using Xunit;

namespace BattleShips.Tests
{
    public class FireControlTests
    {
        private IShipBuilder fakeShipYard;


        protected IBoard board;
        public FireControlTests()
        {
            fakeShipYard = A.Fake<IShipBuilder>();
            A.CallTo(() => fakeShipYard.Build(A<ShipType>.Ignored, A<Position>.Ignored, A<Direction>.Ignored))
                .WithAnyArguments().ReturnsLazily(
                    (ShipType typeOfShip, Position position, Direction direction) => new Ship(typeOfShip, position, direction));

            board = new BattleShipBoard(10, 10, fakeShipYard );

            var startPosition = new Position(0, 0);
            board.Place(ShipType.Frigate, startPosition, Direction.Horizontal);
        }

        [Fact]
        public void WhenFiredOn_ReportsHit()
        {
            var position = new Position(0,0);
            var hitResult = board.FireOn(position);

            Assert.Equal(HitReult.Hit, hitResult);
        }

        [Fact]
        public void WhenFiredOn_ReportsMiss()
        {
            var position = new Position(4, 0);
            var hitResult = board.FireOn(position);

            Assert.Equal(HitReult.Miss, hitResult);
        }

        [Fact]
        public void WhenFiredOn_ReportsSunk()
        {
            var hitResult = board.FireOn(new Position(0,0));
            hitResult = board.FireOn(new Position(1, 0));
            hitResult = board.FireOn(new Position(2, 0));
            hitResult = board.FireOn(new Position(3, 0));

            Assert.Equal(HitReult.Sunk, hitResult);
        }
    }
}
