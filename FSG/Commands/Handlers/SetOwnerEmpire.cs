using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class SetOwnerEmpire : CommandHandler<Commands.SetOwnerEmpire>
    {
        public SetOwnerEmpire(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.SetOwnerEmpire command)
        {
            IOwneable entity = null;
            var empire = _serviceProvider.GlobalState.World.Empires.Find(empire => empire.Id == (EntityId<Empire>)command.EmpireId);

            // TODO: turn this into a method
            if (command.EntityType == EntityType.Agent)
            {
                entity = _serviceProvider.GlobalState.World.Agents.Find(agent => agent.Id == (EntityId<Agent>)command.EntityId);
            }

            if (command.EntityType == EntityType.Region)
            {
                entity = _serviceProvider.GlobalState.World.Regions.Find(agent => agent.Id == (EntityId<Region>)command.EntityId);
            }

            entity.Empire = empire;
        }
    }
}