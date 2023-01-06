using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreatePlayer : CommandHandler<Commands.CreatePlayer>
    {
        public CreatePlayer(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreatePlayer command)
        {
            this._serviceProvider.GlobalState.Entities.Add(new Player
            {
                Id = new EntityId<Player>(),
                Name = command.PlayerName,
                EmpireId = command.EmpireId,
                IsAI = command.IsAI
            });
        }
    }
}