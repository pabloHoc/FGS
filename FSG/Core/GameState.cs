namespace FSG.Core
{
    public class GameState
    {
        public EntityRepository Entities { get; } = new EntityRepository();

        public int Turn { get; private set; } = 0;

        public void nextTurn()
        {
            Turn++;
        }
    }
}