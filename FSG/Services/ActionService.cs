using System;
using FSG.Entities;
using System.Resources;
using FSG.Definitions;
using FSG.Core;
using FSG.Commands;

namespace FSG.Services
{
	public class ActionService
    {
        private readonly ServiceProvider _serviceProvider;

        public ActionService(ServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;
		}

        public bool Allow<T>(T entity, AgentActionDefinition definition) where T : IBaseEntity, IActor
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

        public void Execute<T>(T entity, AgentActionDefinition definition) where T : IEntity<T>, IActor
        {
            if (definition.Actions != null)
            {
                _serviceProvider.ActionProcessor.Process(definition.Actions, entity, entity.Id);
            }
        }
    }
}

