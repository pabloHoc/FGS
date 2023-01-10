using System;
using System.Collections.Generic;
using FSG.Commands;
using FSG.Entities;

namespace FSG.Commands
{
    public class CommandMap
    {
        // TODO: if we have lots of duplicated commands, we should use a switch
        private readonly Dictionary<Type, Dictionary<string, Func<IBaseEntity, dynamic, ICommand>>> _commands =
            new Dictionary<Type, Dictionary<string, Func<IBaseEntity, dynamic, ICommand>>>
            {
                {
                    typeof(Agent), new Dictionary<string, Func<IBaseEntity, dynamic, ICommand>>
                    {
                        { "SetOwnerEmpire", (IBaseEntity scope, dynamic payload) => new SetOwnerEmpire<Agent>((Agent)scope, payload) },
                        { "CreateAgent", (IBaseEntity scope, dynamic payload) => new CreateAgent(payload) }
                    }
                }
            };

        public ICommand Get<T>(string command, T scope, object payload) where T : IBaseEntity
        {
            return _commands[typeof(T)][command](scope, payload);
        }

        public bool Has<T>(string command) where T : IBaseEntity
        {
            Dictionary<string, Func<IBaseEntity, dynamic, ICommand>> entityCommands;

            if (_commands.TryGetValue(typeof(T), out entityCommands))
            {
                return entityCommands.ContainsKey(command);
            }

            return false;
        }
    }
}

