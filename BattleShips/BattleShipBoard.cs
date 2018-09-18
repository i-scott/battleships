using System.Collections.Generic;
using System.Linq;
using BattleShips.Ships;

namespace BattleShips
{
    public class BattleShipBoard : IBoard
    {
        private readonly bool[,] shipsMatrix;
        private readonly IShipBuilder _shipYard;

        public IList<IShip> Ships { get; private set; }

        public BattleShipBoard(int height, int width, IShipBuilder shipYard)
        {
            shipsMatrix = new bool[height, width];
            
            _shipYard = shipYard;

            Ships = new List<IShip>();
        }

        public int Width => shipsMatrix.GetLength(1);

        public int Height => shipsMatrix.GetLength(0);

        public void Place(ShipType typeOfShip, Position startPosition, Direction direction)
        {
            var ship = _shipYard.Build(typeOfShip, startPosition, direction);

            if (direction == Direction.Horizontal)
            {
                if (startPosition.Column + ship.Length > Width) throw new OutOfBoundsException();

                CanShipBePlaced(startPosition, ship);
                
                PlaceShipAcrossTheBoard(startPosition, ship);        
            }
            else
            {
                if ((startPosition.Row + ship.Length) > Height) throw new OutOfBoundsException();

                CanShipBePlaced(startPosition, ship);

                PlaceShipDownTheBoard(startPosition, ship);
            }

            Ships.Add(ship);
        }

        public HitReult FireOn(Position position)
        {
            
            if (shipsMatrix[position.Row, position.Column])
            {
                var ship = Ships.SingleOrDefault(s => s.IsOnPosition(position, Direction.Horizontal) ||
                                            s.IsOnPosition(position, Direction.Vertical));
                ship.Hit++;
                shipsMatrix[position.Row, position.Column] = false;
                return ship.IsSunk() ? HitReult.Sunk : HitReult.Hit;
            }

            return HitReult.Miss;
        }

        private void CanShipBePlaced(Position startPosition, IShip ship)
        {
            var endColumn = startPosition.Column + ship.Length;
            for (var column = startPosition.Column; column < endColumn; column++)
            {
                if( shipsMatrix[startPosition.Row, column] ) throw new OverlapException();
            }

            var endRow = startPosition.Row + ship.Length;
            for (var row = startPosition.Row; row < endRow; row++)
            {
                if (shipsMatrix[row, startPosition.Column]) throw new OverlapException();
            }
        }

        private void PlaceShipDownTheBoard(Position startPosition, IShip ship)
        {
            var endRow = startPosition.Row + ship.Length;
            for (var row = startPosition.Row ;  row < endRow; row++)
            {
                shipsMatrix[row, startPosition.Column] = true;
            }
        }

        private void PlaceShipAcrossTheBoard(Position startPosition, IShip ship)
        {
            var endColumn = startPosition.Column + ship.Length;
            for (var column = startPosition.Column; column < endColumn; column++)
            {
                shipsMatrix[startPosition.Row, column] = true;
            }
        }


    }
}