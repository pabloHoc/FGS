using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ExecuteEntityAction<T> : CommandHandler<Commands.ExecuteEntityAction<T>> where T : IEntity<T>, IActor, ILocatable
    {
        public ExecuteEntityAction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ExecuteEntityAction<T> command)
        {
            var entity = _serviceProvider.GlobalState.Entities.Get(command.EntityId);

            if (command.ActionType == ActionType.Spell)
            {
                var spell = _serviceProvider.Definitions.Get<SpellDefinition>(command.ActionName);
                _serviceProvider.Services.SpellService.Execute(entity, spell);
            } else if (command.ActionName == "Move")
            {
                _serviceProvider.Dispatcher.Dispatch(new Commands.SetLocation<T>
                {
                    EntityId = entity.Id,
                    EntityType = EntityType.Agent, // TODO: review this
                    RegionId = new EntityId<Region>(command.Payload)
                });
            }
        } 
    }
}

