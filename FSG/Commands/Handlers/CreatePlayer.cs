using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreatePlayerHandler : ICommandHandler<CreatePlayer>
    {
        private ServiceProvider _serviceProvider { get; }

        public CreatePlayerHandler(ServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public void Handle(CreatePlayer command)
        {
            this._serviceProvider.GlobalState.Entities.Add(new Player
            {
                Id = new EntityId<Player>(),
                Name = command.Name,
                EmpireId = command.EmpireId,
                IsAI = command.IsAI
            });
        }
    }
}