using BattleShips.Ships;

namespace BattleShips
{
    public interface IBoard
    {
        int Height { get; }
        int Width { get; }

        void Place(ShipType typeOfShip, Position startPosition, Direction direction);

        HitReult FireOn(Position position);
    }
}