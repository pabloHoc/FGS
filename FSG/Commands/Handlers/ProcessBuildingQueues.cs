using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
	public class ProcessBuildingQueues : CommandHandler<Commands.ProcessBuildingQueues>
	{
        public ProcessBuildingQueues(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ProcessBuildingQueues command)
        {
            var regions = _serviceProvider.GlobalState.World.Regions;

            foreach(var region in regions)
            {
                BuildingQueueItem buildingQueueItem = null;
                if (region.BuildingQueue.TryPeek(out buildingQueueItem))
                {
                    buildingQueueItem.RemainingTurns--;

                    if (buildingQueueItem.RemainingTurns == 0)
                    {
                        _serviceProvider.Dispatcher.Dispatch(new Commands.BuildBuildingFromQueue
                        {
                            RegionId = region.Id
                        });
                    }
                }
            }
        }
    }
}

