using System.Collections.Generic;

namespace BattleShips
{
    public class BoardFactory
    {
       
        public IBoard Create(int boardSize = 10)
        {
            return new BattleShipBoard(boardSize,boardSize);
        }

    }
}