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
            var land = _serviceProvider.GlobalState.Entities.Get(command.LandId);
            var building = land.BuildingQueue.Dequeue();
            _serviceProvider.Dispatcher.Dispatch(new Commands.BuildBuilding
            {
                LandId = land.Id,
                BuildingName = building.Name
            });
        }
    }
}

