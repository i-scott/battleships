namespace BattleShips
{
    public class BoardFactory
    {
       
        public IBoard Create()
        {
            return new BattleShipBoard();
        }
    }
}