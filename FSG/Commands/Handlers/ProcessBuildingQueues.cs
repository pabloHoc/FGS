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
            var lands = _serviceProvider.GlobalState.Entities.GetAll<Land>();

            foreach(var land in lands)
            {
                var buildingQueueItem = land.BuildingQueue.Peek();
                buildingQueueItem.RemainingTurns--;

                if (buildingQueueItem.RemainingTurns == 0)
                {
                    _serviceProvider.Dispatcher.Dispatch(new Commands.BuildBuildingFromQueue
                    {
                        LandId = land.Id
                    });
                }
            }
        }
    }
}

