using System;
using FSG.Core;
using FSG.Entities;

// TODO: Should we process building logic here? (costs, etc)

namespace FSG.Commands.Handlers
{
    public class BuildBuilding : CommandHandler<Commands.BuildBuilding>
    {
        public BuildBuilding(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.BuildBuilding command)
        {
            var land = _serviceProvider.GlobalState.Entities.Get(command.LandId);
            land.Buildings.Add(command.BuildingName);
        }
    }
}

