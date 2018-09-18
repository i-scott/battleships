namespace BattleShips
{
    public interface IBoardFactory
    {
        IBoard Create(int boardSize = 10);
    }
}