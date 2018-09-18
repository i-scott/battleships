using BattleShips.Ships;

namespace BattleShips
{
    public interface IShipBuilder
    {
        IShip Build(ShipType typeOfShip, Position position, Direction direction);
    }
}