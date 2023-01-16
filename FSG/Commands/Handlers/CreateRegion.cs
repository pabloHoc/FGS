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
                Name = command.RegionName,
                EmpireId = command.EmpireId,
                X = command.X,
                Y = command.Y,
                ConnectedTo = new List<EntityId<Region>>(),
                Capital = new Capital(),
                BuildingQueue = new Queue<BuildingQueueItem>()
            });
        }
    }
}

