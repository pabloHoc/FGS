namespace FSG.Core
{
    public class GameState
    {
        public EntityRepository Entities { get; } = new EntityRepository();

        public int Turn { get; set; } = 0;
    }
}