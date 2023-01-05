using FSG.Conditions;
using FSG.Services;

namespace FSG.Core
{
    public class ServiceProvider
    {
        public GameState GlobalState { get; }

        public DefinitionRepository Definitions { get; }

        public CommandDispatcher Dispatcher { get; }

        public FSG.Scopes.Scopes Scopes { get; }

        public ConditionValidator ConditionValidator { get; }

        public ServiceMap Services { get; }

        public ServiceProvider()
        {
            GlobalState = new GameState();
            Definitions = new DefinitionRepository();
            Dispatcher = new CommandDispatcher(this);
            Scopes = new FSG.Scopes.Scopes(GlobalState);
            ConditionValidator = new ConditionValidator(this);
            Services = new ServiceMap(this);
        }
    }
}