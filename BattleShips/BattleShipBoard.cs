namespace BattleShips
{
    public class BattleShipBoard : IBoard
    {
        private bool[,] shipsMatrix;

        public BattleShipBoard(int height, int width)
        {
            shipsMatrix = new bool[height, width];
        }

        public int Width => shipsMatrix.GetLength(1);
        public int Height => shipsMatrix.GetLength(0);


    }
}