using System;
using FSG.Entities;
using System.Resources;
using FSG.Definitions;
using FSG.Core;
using FSG.Commands;

namespace FSG.Services
{
	public class SpellService
    {
        private readonly ServiceProvider _serviceProvider;

        public SpellService(ServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;
		}

        public bool Allow<T>(T entity, SpellDefinition definition) where T : IBaseEntity, IActor
        {
            if (definition.Conditions != null)
            {
                if (!_serviceProvider.ConditionValidator.isValid(definition.Conditions, entity))
                {
                    return false;
                }
            }

            return true;
        }

        public void Execute<T>(T entity, SpellDefinition definition) where T : IEntity<T>, IActor
        {
            _serviceProvider.Dispatcher.Dispatch(new CreateSpell
            {
                SpellName = definition.Name,
                Duration = definition.Duration
            });

            var spellId = _serviceProvider.GlobalState.Entities.GetLastAddedEntityId<Spell>();

            if (definition.Actions != null)
            {
                _serviceProvider.ActionProcessor.Process(definition.Actions, entity, spellId);
            }
        }
    }
}

