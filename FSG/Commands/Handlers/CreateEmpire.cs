using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateEmpire : CommandHandler<Commands.CreateEmpire>
    {
        public CreateEmpire(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateEmpire command)
        {
            _serviceProvider.GlobalState.Entities.Add(new Empire
            {
                Id = new EntityId<Empire>(),
                Name = command.Name
            });
        }
    }
}

