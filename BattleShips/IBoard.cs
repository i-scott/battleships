namespace BattleShips
{
    public interface IBoard
    {
        int Height { get; }
        int Width { get; }

        void Place(IShip ship, Position startPosition, Direction direction);
    }
}