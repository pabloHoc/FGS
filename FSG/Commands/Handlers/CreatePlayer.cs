using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreatePlayer : CommandHandler<Commands.CreatePlayer>
    {
        public CreatePlayer(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreatePlayer command)
        {
            var empire = _serviceProvider.GlobalState.World.Empires.Find(empire => empire.Id == command.EmpireId);

            var player = new Player
            {
                Id = new EntityId<Player>(),
                Name = command.PlayerName,
                Empire = empire,
                IsAI = command.IsAI,
            };

            if (player.IsAI)
            {
                player.AI = new AI.AI(player, _serviceProvider);
            }

            this._serviceProvider.GlobalState.World.Players.Add(player);
            this._serviceProvider.GlobalState.World.LastAddedEntityId = player.Id;
        }
    }
}