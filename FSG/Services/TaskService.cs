using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.UtilityAI;

namespace FSG.Services
{
    public class TaskService
    {
        private readonly ServiceProvider _serviceProvider;

        public TaskService(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Func<S, T, bool> GetValidator<S, T>(TaskDefinition definition)
            where S : IState
            where T : IBaseEntity
        {
            return (S gameState, T target) =>
            {
                if (definition.Conditions != null)
                {
                    return _serviceProvider.ConditionValidator.isValid(definition.Conditions, target);
                }
                return true;
            };
        }

        public List<Scorer> GetScorers(TaskDefinition definition)
        {
            var scorers = new List<Scorer>();

            foreach (var scorerName in definition.Scorers)
            {
                scorers.Add(_serviceProvider.Definitions.Get<ScorerDefinition>(scorerName).Scorer);
            }

            return scorers;
        }

        public List<IBaseEntity> GetTargets<T>(GameState gameState, T target, TaskDefinition definition)
            where T : IEntity<T>
        {
            var scopes = new FSG.Scopes.Scopes(gameState);
            var scope = scopes.GetFrom(definition.Target, target);
            return new List<IBaseEntity>() { scope };
        }
    }
}

