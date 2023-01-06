using System;
using FSG.Entities;
using System.Resources;
using FSG.Definitions;
using FSG.Core;

namespace FSG.Services
{
	public class SpellService
    {
        private readonly ServiceProvider _serviceProvider;

        public SpellService(ServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;
		}

        public bool Allow(IActorEntity entity, SpellDefinition definition)
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

        public void Execute<T>(T entity, SpellDefinition definition) where T : IEntity<T>, IActorEntity
        {
            if (definition.Actions != null)
            {
                _serviceProvider.ActionProcessor.Process(definition.Actions, entity);
            }
        }
    }
}

