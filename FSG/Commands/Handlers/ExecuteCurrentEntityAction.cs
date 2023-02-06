using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ExecuteCurrentEntityAction : CommandHandler<Commands.ExecuteCurrentEntityAction>
    {
        public ExecuteCurrentEntityAction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ExecuteCurrentEntityAction command)
        {
            var entity = _serviceProvider.GlobalState.World.Agents.Find(agent => agent.Id == (EntityId<Agent>)command.EntityId);

            var action = entity.Actions.Dequeue();

            _serviceProvider.Dispatcher.Dispatch(new Commands.ExecuteEntityAction
            {
                SourceEntityId = entity.Id,
                ActionName = action.Name,
                ActionType = action.ActionType,
                Payload = action.Payload
            });
        }
    }
}

