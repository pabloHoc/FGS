using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class QueueEntityAction: CommandHandler<Commands.QueueEntityAction>
    {
        public QueueEntityAction(ServiceProvider serviceProvider)
            : base(serviceProvider) { }

        public override void Handle(Commands.QueueEntityAction command)
        {
            var agent = _serviceProvider.GlobalState.World.Agents
                .Find(agent => agent.Id == (EntityId<Agent>)command.SourceEntityId);
            var actionDefinition = _serviceProvider.Definitions.Get<AgentActionDefinition>(command.ActionName);

            agent.Actions.Enqueue(new ActionQueueItem
            {
                Name = command.ActionName,
                ActionType = ActionType.Action,
                RemainingTurns = actionDefinition.BaseExecutionTime,
                Payload = command.Payload
            });
        }
    }
}

