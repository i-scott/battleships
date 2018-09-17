using System.Collections.Generic;

namespace BattleShips
{
    public class BattleShipBoard : IBoard
    {
        private readonly bool[,] shipsMatrix;

        public IList<PlacedShip> Ships { get; private set; }

        public BattleShipBoard(int height, int width)
        {
            shipsMatrix = new bool[height, width];

            Ships = new List<PlacedShip>();
        }

        public int Width => shipsMatrix.GetLength(1);

        public int Height => shipsMatrix.GetLength(0);

        public void Place(IShip ship, Position startPosition, Direction direction)
        {
            if (direction == Direction.Horizontal)
            {
                if ((startPosition.Column + ship.Length) > Width) throw new OutOfBoundsException();

                PlaceShipAcrossTheBoard(startPosition, ship);        
            }
            else
            {

                if ((startPosition.Row + ship.Length) > Height) throw new OutOfBoundsException();

                PlaceShipDownTheBoard(startPosition, ship);
            }

            Ships.Add(PlacedShip.Create(ship, startPosition, direction));
        }

        private void PlaceShipDownTheBoard(Position startPosition, IShip ship)
        {
            for (var row = startPosition.Row ;  row < ship.Length; row++)
            {
                shipsMatrix[row, startPosition.Column] = true;
            }
        }

        private void PlaceShipAcrossTheBoard(Position startPosition, IShip ship)
        {
            for (var column = startPosition.Column; column < ship.Length; column++)
            {
                shipsMatrix[startPosition.Row, column] = true;
            }
        }


    }
}