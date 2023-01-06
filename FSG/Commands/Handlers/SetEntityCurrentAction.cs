using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class SetEntityCurrentAction<T> : CommandHandler<Commands.SetEntityCurrentAction<T>> where T : IEntity<T>, IActorEntity
    {
        public SetEntityCurrentAction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.SetEntityCurrentAction<T> command)
        {
            var entity = _serviceProvider.GlobalState.Entities.Get(command.EntityId);
            entity.Actions.Clear();
            entity.Actions.Enqueue(command.NewCurrentAction);
        }
    }
}