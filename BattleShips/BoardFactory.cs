using System.Collections.Generic;

namespace BattleShips
{
    public class BoardFactory : IBoardFactory
    {
        private readonly IShipBuilder _shipYard;

        public BoardFactory(IShipBuilder shipYard)
        {
            _shipYard = shipYard;
        }

        public IBoard Create(int boardSize = 10)
        {
            return new BattleShipBoard(boardSize,boardSize, _shipYard);
        }

    }
}