using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateRegion : CommandHandler<Commands.CreateRegion>
    {
        public CreateRegion(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateRegion command)
        {
            _serviceProvider.GlobalState.Entities.Add(new Region
            {
                Id = new EntityId<Region>(),
                Name = command.Name,
                EmpireId = command.EmpireId,
                X = command.X,
                Y = command.Y,
                ConnectedTo = new List<EntityId<Region>>(),
                Modifiers = new List<Modifier>()
            });
        }
    }
}

