using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
	public class EndTurn : CommandHandler<Commands.EndTurn>
	{
        public EndTurn(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.EndTurn command)
        {
            // Should we process AI here, and move everythign else to StartTurn?
            _serviceProvider.Dispatcher.Dispatch(new Commands.ProcessPlayerAIs());
            _serviceProvider.Dispatcher.Dispatch(new Commands.ProcessSpells());
            _serviceProvider.Dispatcher.Dispatch(new Commands.ProcessEntityActions<Agent>());
            _serviceProvider.Dispatcher.Dispatch(new Commands.ProcessBuildingQueues());
            _serviceProvider.Dispatcher.Dispatch(new Commands.ComputeProduction());
            _serviceProvider.Dispatcher.Dispatch(new Commands.GenerateResources());

            _serviceProvider.GlobalState.nextTurn();
        }
    }
}

