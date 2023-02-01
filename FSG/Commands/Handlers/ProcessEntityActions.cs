using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
	public class ProcessEntityActions : CommandHandler<Commands.ProcessEntityActions>
	{
        public ProcessEntityActions(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ProcessEntityActions command)
        {
            var entities = _serviceProvider.GlobalState.World.Agents;

            foreach(var entity in entities)
            {
                ActionQueueItem action = null;
                if (entity.Actions.TryPeek(out action))
                {
                    action.RemainingTurns--;

                    if (action.RemainingTurns == 0)
                    {
                        _serviceProvider.Dispatcher.Dispatch(new Commands.ExecuteCurrentEntityAction
                        {
                            EntityId = entity.Id,
                            EntityType = entity.EntityType
                        });
                    }
                }
            }
        }
    }
}

