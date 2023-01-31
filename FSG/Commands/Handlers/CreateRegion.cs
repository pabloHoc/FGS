using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateRegion : CommandHandler<Commands.CreateRegion>
    {
        public CreateRegion(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateRegion command)
        {
            var resources = _serviceProvider.Definitions.GetAll<ResourceDefinition>()
                .FindAll(resource => resource.Scope == Scopes.Scope.Region);
            var resourceBlock = new ResourceBlock();

            foreach (var resource in resources)
            {
                resourceBlock.Resources.Add(resource.Name, 0);
                resourceBlock.Production.Add(resource.Name, 0);
                resourceBlock.Upkeep.Add(resource.Name, 0);
            }

            var region = new Region
            {
                Id = new EntityId<Region>(),
                Name = command.RegionName,
                EmpireId = command.EmpireId,
                X = command.X,
                Y = command.Y,
                ConnectedTo = new List<EntityId<Region>>(),
                Capital = new Capital(),
                BuildingQueue = new Queue<BuildingQueueItem>(),
                Resources = resourceBlock,
                Lands = new List<Land>(),
                Pops = new List<Pop>()
            };

            _serviceProvider.GlobalState.Entities.Add(region);

            if (command.EmpireId != null)
            {
                _serviceProvider.GlobalState.Entities.Get(command.EmpireId).Regions.Add(region);
            }
        }
    }
}

