namespace BattleShips
{
    public class PlacedShip
    {
        public IShip Ship { get; }
        public Position PlacedPosition { get; }
        public Direction Direction { get; }

        protected PlacedShip(IShip ship, Position placedPosition, Direction direction)
        {
            Ship = ship;
            PlacedPosition = placedPosition;
            Direction = direction;
        }

        public static PlacedShip Create(IShip ship, Position startPosition, Direction direction)
        {
            return new PlacedShip(ship, startPosition, direction);
        }
    }
}