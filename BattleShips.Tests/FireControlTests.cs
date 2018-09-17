using FakeItEasy;
using Xunit;

namespace BattleShips.Tests
{
    public class FireControlTests
    {
        protected IBoard board = new BattleShipBoard(10, 10);

        public FireControlTests()
        {
            
            var mockShip = A.Fake<IShip>();
            A.CallTo(() => mockShip.Length).Returns(4);

            var startPosition = new Position(0, 0);
            board.Place(mockShip, startPosition, Direction.Horizontal);
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
    }
}
