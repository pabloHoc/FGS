using System;
using System.Collections.Generic;
using FSG.Commands;
using FSG.Entities;

namespace FSG.Commands
{
    public class CommandMap
    {
        private readonly Dictionary<string, Func<IBaseEntity, dynamic, ICommand>> _agentCommands = new ()
        {
            { "CreateAgent", (IBaseEntity scope, dynamic payload) => new CreateAgent(payload) }
        };

        private readonly Dictionary<string, Func<IBaseEntity, dynamic, ICommand>> _regionCommands = new()
        {
            { "CreatePop", (IBaseEntity scope, dynamic payload) => new CreatePop(payload) },
            { "SetOwnerEmpire", (IBaseEntity scope, dynamic payload) => new SetOwnerEmpire(scope, payload) }
        };

        private Dictionary<string, Func<IBaseEntity, dynamic, ICommand>> GetCommandMapFor<T>(T entity) where T : IBaseEntity
        {
            switch (entity.EntityType)
            {
                case EntityType.Agent:
                    return _agentCommands;
                case EntityType.Region:
                    return _regionCommands;
            }
            return null;
        }

        public ICommand Get<T>(string command, T scope, object payload) where T : IBaseEntity
        {
            return GetCommandMapFor(scope)[command](scope, payload);
        }

        public bool Has<T>(string command, T scope) where T : IBaseEntity
        {
            var commandMap = GetCommandMapFor(scope);
            return (bool)(commandMap?.ContainsKey(command));
        }
    }
}

