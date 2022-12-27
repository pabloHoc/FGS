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
            _serviceProvider.GlobalState.Entities.Add(new Agent
            {
                Id = new EntityId<Agent>(),
                Name = command.Name,
                EmpireId = command.EmpireId,
                RegionId = command.RegionId,
                Modifiers = new List<Modifier>(),
                Actions = new Queue<ActionQueueItem>()
            });
        }
    }
}

