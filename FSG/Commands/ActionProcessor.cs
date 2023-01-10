using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;
using FSG.Extensions;
using FSG.Scopes;

namespace FSG.Commands
{
	public class ActionProcessor
	{
		private class ScopeContext
		{
			public IBaseEntity Root { get; set; }

			public IBaseEntity This { get; set; }

            public IBaseEntity Prev { get; set; }
		}

        private readonly CommandMap _commandMap = new CommandMap();

        private readonly ServiceProvider _serviceProvider;

        public ActionProcessor(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private void ProcessActions<T, U, V>(T actions, U scope, ScopeContext scopeContext, V sourceId)
            where T : Dictionary<string, object>
            where U : IBaseEntity
            where V : IEntityId
        {
			scopeContext.This = scope;
            Scope scopeKey;

            foreach (var entry in actions)
            {
                // if it's command
                if (_commandMap.Has<U>(entry.Key))
                {
                    var payload = entry.Value;
                    var scopeContextPayload = scopeContext
                        .GetType()
                        .GetProperty(((string)payload).CapitalizeFirstLetter())
                        .GetValue(scopeContext);

                    // Try to get ScopeContext value and send it as command payload 
                    if (scopeContextPayload != null)
                    {
                        payload = scopeContextPayload;
                    }

                    var command = _commandMap.Get(entry.Key, scope, payload);

                    _serviceProvider.Dispatcher.Dispatch(command);
                }
                // if it's scope
                else if (Enum.TryParse(entry.Key.CapitalizeFirstLetter(), out scopeKey))
                {
                    var newScope = _serviceProvider.Scopes.GetFrom(scopeKey, scope);
                    scopeContext.Prev = scope;
                    ProcessActions((Dictionary<string, object>)entry.Value, newScope, scopeContext, sourceId);
                }
                // if it's modifiers block
                else if (entry.Key == "Modifiers")
                {
                    ProcessActions((Dictionary<string, object>)entry.Value, scope, scopeContext, sourceId);
                }
                // if it's modifier block type
                else if(entry.Key == "Add" || entry.Key == "Mult" || entry.Key == "Reduction")
                {
                    ModifierType modifierType;

                    if (Enum.TryParse(entry.Key, out modifierType))
                    {
                        foreach (var modifier in (Dictionary<string, object>)entry.Value)
                        {
                            _serviceProvider.Dispatcher.Dispatch(new CreateModifier
                            {
                                ModifierName = modifier.Key,
                                ModifierType = modifierType,
                                Value = (int)modifier.Value,
                                TargetId = ((dynamic)scope).Id,
                                SourceId = sourceId
                            });
                        }

                    }
                }
            }
        }

        public void Process<T, U>(Actions actions, T scope, U sourceId)
            where T : IEntity<T>
            where U : IEntityId
        {
            ProcessActions(actions, scope, new ScopeContext
                {
                    Root = scope,
                    This = scope
                },
                sourceId
            );
		}
	}
}

