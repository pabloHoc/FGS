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
            // TODO: check orden here
            _serviceProvider.Dispatcher.Dispatch(new Commands.ComputeEmpiresProduction());
            _serviceProvider.Dispatcher.Dispatch(new Commands.ComputeRegionsProduction());
            _serviceProvider.Dispatcher.Dispatch(new Commands.UpdateEmpiresResources());
            _serviceProvider.Dispatcher.Dispatch(new Commands.UpdateRegionsResources());
            _serviceProvider.Dispatcher.Dispatch(new Commands.ProcessResourceLevels());

            _serviceProvider.GlobalState.nextTurn();
        }
    }
}

