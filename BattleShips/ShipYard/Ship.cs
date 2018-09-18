using System;

namespace BattleShips.Ships
{
    public class Ship : IShip
    {
        private readonly ShipType _type;
        private readonly Position _position;

        public Ship(ShipType type, Position position, Direction direction)
        {
            _type = type;
            _position = position;
            Direction = direction;
        }

        public int Length => (int)_type;

        public int Hit { get; set; }

        public Direction Direction { get; }

        public bool IsSunk()
        {
            return Hit == Length;;
        }

        public bool IsOnPosition(Position position, Direction direction)
        {
            if (direction == Direction.Horizontal)
            {
                return position.Row == this._position.Row &&
                       position.Column >= this._position.Column &&
                       position.Column <= (_position.Column + Length);
            }

            return position.Column == this._position.Column &&
                   position.Row >= this._position.Row &&
                   position.Row <= (_position.Row + Length);

        }
    }
}
