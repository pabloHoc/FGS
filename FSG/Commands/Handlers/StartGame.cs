using System;
using FSG.Core;

namespace FSG.Commands.Handlers
{
    public class StartGame : CommandHandler<Commands.StartGame>
    {
        public StartGame(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.StartGame command)
        {
            _serviceProvider.Dispatcher.Dispatch(new Commands.ComputeProduction());
        }
    }
}

