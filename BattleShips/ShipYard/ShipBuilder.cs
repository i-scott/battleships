using System;
using System.Collections.Generic;
using System.Text;
using BattleShips.Ships;

namespace BattleShips.ShipYard
{
    public class ShipBuilder : IShipBuilder
    {
        public IShip Build(ShipType typeOfShip, Position position, Direction direction)
        {
            return new Ship(typeOfShip,position,direction);
        }
    }
}
