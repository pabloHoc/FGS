using System;
using FSG.Core;

namespace FSG.Commands.Handlers
{
	public class EndTurn : CommandHandler<Commands.EndTurn>
	{
        public EndTurn(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.EndTurn command)
        {
            _serviceProvider.Dispatcher.Dispatch(new Commands.ProcessBuildingQueues());
            _serviceProvider.Dispatcher.Dispatch(new Commands.UpdateProduction());
            _serviceProvider.Dispatcher.Dispatch(new Commands.GenerateResources());

            _serviceProvider.GlobalState.nextTurn();
        }
    }
}

