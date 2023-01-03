using System;
using FSG.Core;
using FSG.Data;
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
                Id = new EntityId<BuildingQueueItem>(),
                Name = buildingDefinition.Name,
                RemainingTurns = buildingDefinition.BaseBuildTime
            });

            // Apply building costs

            var empire = _serviceProvider.GlobalState.Entities.Get(command.EmpireId);
        }
    }
}

