using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class AddBuildingToQueue : CommandHandler<Commands.AddBuildingtoQueue>
    {
        public AddBuildingToQueue(ServiceProvider serviceProvider)
            : base(serviceProvider) { }

        public override void Handle(AddBuildingtoQueue command)
        {
            var land = _serviceProvider.GlobalState.Entities.Get(command.LandId);
            var buildingDefinition = _serviceProvider.Definitions
                .Get<BuildingDefinition>(command.BuildingName);

            land.BuildingQueue.Enqueue(new BuildingQueueItem
            {
                Name = buildingDefinition.Name,
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

