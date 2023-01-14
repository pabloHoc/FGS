using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ExecuteCurrentEntityAction<T> : CommandHandler<Commands.ExecuteCurrentEntityAction<T>> where T : IEntity<T>, IActor
    {
        public ExecuteCurrentEntityAction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ExecuteCurrentEntityAction<T> command)
        {
            var entity = _serviceProvider.GlobalState.Entities.Get(command.EntityId);
            var action = entity.Actions.Dequeue();
            _serviceProvider.Dispatcher.Dispatch(new Commands.ExecuteEntityAction<T>
            {
                EntityId = entity.Id,
                ActionName = action.Name,
                ActionType = action.ActionType,
                Payload = action.Payload
            });
        }
    }
}

