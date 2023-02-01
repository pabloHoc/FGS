using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ProcessPlayerAIs : CommandHandler<Commands.ProcessPlayerAIs>
    {
        public ProcessPlayerAIs(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ProcessPlayerAIs command)
        {
            var aiPlayers = _serviceProvider.GlobalState.World.Players
                .FindAll(player => player.IsAI);

            foreach (var player in aiPlayers)
            {
                player.AI.PlayTurn();
            }
        }
    }
}

