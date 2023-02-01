using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class SetEntityCurrentAction : CommandHandler<Commands.SetEntityCurrentAction>
    {
        public SetEntityCurrentAction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.SetEntityCurrentAction command)
        {
            var entity = _serviceProvider.GlobalState.World.Agents.Find(agent => agent.Id == (EntityId<Agent>)command.EntityId);
            entity.Actions.Clear();
            entity.Actions.Enqueue(command.NewCurrentAction);
        }
    }
}