using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class QueueBuilding : CommandHandler<Commands.QueueBuilding>
    {
        public QueueBuilding(ServiceProvider serviceProvider)
            : base(serviceProvider) { }

        public override void Handle(Commands.QueueBuilding command)
        {
            var region = _serviceProvider.GlobalState.World.Regions.Find(region => region.Id == command.RegionId);
            var buildingDefinition = _serviceProvider.Definitions.Get<BuildingDefinition>(command.BuildingName);

            region.BuildingQueue.Enqueue(new BuildingQueueItem
            {
                Name = buildingDefinition.Name,
                BuildingType = command.BuildingType,
                LandId = command.LandId,
                RegionId = command.RegionId,
                RemainingTurns = buildingDefinition.BaseBuildTime
            });

            // Apply building costs
            // TODO: add economic category modifiers

            var empire = _serviceProvider.GlobalState.World.Empires.Find(empire => empire.Id == command.EmpireId);

            foreach (var resource in buildingDefinition.Resources.Cost)
            {
                empire.Resources.Resources[resource.Key] -= resource.Value;
            }
        }
    }
}

