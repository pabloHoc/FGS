using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class BuildBuildingFromQueue : CommandHandler<Commands.BuildBuildingFromQueue>
    {
        public BuildBuildingFromQueue(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.BuildBuildingFromQueue command)
        {
            var region = _serviceProvider.GlobalState.Entities.Get(command.RegionId);
            var building = region.BuildingQueue.Dequeue();

            _serviceProvider.Dispatcher.Dispatch(new Commands.BuildBuilding
            {
                LandId = building.LandId,
                RegionId = command.RegionId,
                BuildingName = building.Name,
                BuildingType = building.BuildingType
            });
        }
    }
}

