using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class AddBuildingToQueue : CommandHandler<Commands.AddBuildingToQueue>
    {
        public AddBuildingToQueue(ServiceProvider serviceProvider)
            : base(serviceProvider) { }

        public override void Handle(Commands.AddBuildingToQueue command)
        {
            var region = _serviceProvider.GlobalState.Entities.Get(command.RegionId);
            IBuildingDefinition buildingDefinition = command.BuildingType == BuildingType.LandBuilding
                ? _serviceProvider.Definitions.Get<BuildingDefinition>(command.BuildingName)
                : _serviceProvider.Definitions.Get<DistrictDefinition>(command.BuildingName);

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

            var empire = _serviceProvider.GlobalState.Entities.Get(command.EmpireId);

            foreach (var resource in buildingDefinition.Resources.Cost)
            {
                empire.Resources[resource.Key] -= resource.Value;
            }
        }
    }
}

