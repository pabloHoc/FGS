using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
	public class ProcessEntityActions<T> : CommandHandler<Commands.ProcessEntityActions<T>> where T : IEntity<T>, IActorEntity
	{
        public ProcessEntityActions(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ProcessEntityActions<T> command)
        {
            var entities = _serviceProvider.GlobalState.Entities.GetAll<T>();

            foreach(var entity in entities)
            {
                ActionQueueItem action = null;
                if (entity.Actions.TryPeek(out action))
                {
                    action.RemainingTurns--;

                    if (action.RemainingTurns == 0)
                    {
                        _serviceProvider.Dispatcher.Dispatch(new Commands.ExecuteCurrentEntityAction<T>
                        {
                            EntityId = entity.Id
                        });
                    }
                }
            }
        }
    }
}

