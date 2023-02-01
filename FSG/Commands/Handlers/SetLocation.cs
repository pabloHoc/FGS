using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class SetLocation : CommandHandler<Commands.SetLocation>
    {
        public SetLocation(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.SetLocation command)
        {
            var entity = _serviceProvider.GlobalState.World.Agents.Find(agent => agent.Id == (EntityId<Agent>)command.EntityId);
            var region = _serviceProvider.GlobalState.World.Regions.Find(region => region.Id == (EntityId<Region>)command.RegionId);
            entity.Region = region;
        }
    }
}