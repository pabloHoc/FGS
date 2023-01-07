using System;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands.Handlers
{
    public class ExecuteEntityAction<T> : CommandHandler<Commands.ExecuteEntityAction<T>> where T : IEntity<T>, IActor
    {
        public ExecuteEntityAction(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Handle(Commands.ExecuteEntityAction<T> command)
        {
            if (command.ActionType == ActionType.Spell)
            {
                var entity = _serviceProvider.GlobalState.Entities.Get(command.EntityId);
                var spell = _serviceProvider.Definitions.Get<SpellDefinition>(command.ActionName);
                _serviceProvider.Services.SpellService.Execute(entity, spell);
            }
        }
    }
}

