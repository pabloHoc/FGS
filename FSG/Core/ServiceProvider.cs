namespace FSG.Core
{
    public class ServiceProvider
    {
        public GameState GlobalState { get; }

        public DefinitionRepository Definitions { get; }

        public CommandDispatcher Dispatcher { get; }

        public ServiceProvider()
        {
            GlobalState = new GameState();
            Definitions = new DefinitionRepository();
            Dispatcher = new CommandDispatcher(this);
        }
    }
}