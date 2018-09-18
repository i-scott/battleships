namespace BattleShips
{
    public interface IShip
    {
        int Length { get;  }
        int Hit { get; set; }

        bool IsSunk();

        bool IsOnPosition(Position position, Direction direction);
    }
}