using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class CreateAgent : CommandHandler<Commands.CreateAgent>
    {
        public CreateAgent(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.CreateAgent command)
        {
            var empire = _serviceProvider.GlobalState.World.Empires.Find(empire => empire.Id == command.EmpireId);
            var region = _serviceProvider.GlobalState.World.Regions.Find(region => region.Id == command.RegionId);

            var agent = new Agent
            {
                Id = new EntityId<Agent>(),
                Name = command.AgentName,
                Empire = empire,
                Region = region,
                Actions = new Queue<ActionQueueItem>()
            };

            _serviceProvider.GlobalState.World.Agents.Add(agent);
        }
    }
}

