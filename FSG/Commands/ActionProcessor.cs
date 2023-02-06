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

        private readonly CommandMap _commandMap = new();

        private readonly ServiceProvider _serviceProvider;

        public ActionProcessor(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private bool IsModifierBlock(string str)
        {
            return str == "Modifiers";
        }

        private bool IsModifierTypeBlock(string str)
        {
            return str == "Add" || str == "Mult" || str == "Reduction";
        }

        private object GetEntityFromScopeContextKey(ScopeContext scopeContext, string key)
        {
            var property = scopeContext.GetType().GetProperty(key);
            return property?.GetValue(scopeContext);
        }

        private void ProcessModifiers(KeyValuePair<string, object> entry, IBaseEntity scope, IEntityId sourceId)
        {
            if (Enum.TryParse(entry.Key, out ModifierType modifierType))
            {
                foreach (var modifier in (Dictionary<string, object>)entry.Value)
                {
                    _serviceProvider.Dispatcher.Dispatch(new CreateModifier
                    {
                        ModifierName = modifier.Key,
                        ModifierType = modifierType,
                        Value = Convert.ToDecimal(modifier.Value),
                        TargetId = ((dynamic)scope).Id,
                        TargetType = ((dynamic)scope).EntityType,
                        SourceId = sourceId
                    });
                }
            }

        }

        private void ProcessCommands(KeyValuePair<string, object> commandEntry, IBaseEntity scope, ScopeContext scopeContext)
        {
            // Clone the payload to avoid modifying the original reference
            var payload = new Dictionary<string, object> { { commandEntry.Key, commandEntry.Value } };

            foreach (var payloadEntry in payload)
            {
                if (payloadEntry.Value is string payloadValue)
                {
                    // TODO: review if this is payload context only (this, root, prev) or if it should
                    // be scope directly. commandPayload can be something like "This.Id"
                    var scopeContextPayload = GetEntityFromScopeContextKey(scopeContext, payloadValue.Split('.')[0]);

                    if (scopeContextPayload != null)
                    {
                        payload[payloadEntry.Key] = payloadValue.Contains(".Id")
                            ? ((dynamic)scopeContextPayload).Id
                            : scopeContextPayload;
                    }
                }
            }

            var command = _commandMap.Get(commandEntry.Key, scope, payload);

            _serviceProvider.Dispatcher.Dispatch(command);
        }

        private void ProcessActions<T, U, V>(T actions, U scope, ScopeContext scopeContext, V sourceId)
            where T : Dictionary<string, object>
            where U : IBaseEntity
            where V : IEntityId
        {
			scopeContext.This = scope;

            foreach (var entry in actions)
            {
                if (_commandMap.Has(entry.Key, scope))
                {
                    ProcessCommands(entry, scope, scopeContext);
                }
                // if it's scope
                else if (Enum.TryParse(entry.Key.CapitalizeFirstLetter().Split('.')[0], out Scope scopeKey))
                {
                    var newScope = _serviceProvider.Scopes.GetFrom(scopeKey, scope);
                    scopeContext.Prev = scope;
                    ProcessActions((Dictionary<string, object>)entry.Value, newScope, scopeContext, sourceId);
                }
                else if (IsModifierBlock(entry.Key))
                {
                    ProcessActions((Dictionary<string, object>)entry.Value, scope, scopeContext, sourceId);
                }
                else if (IsModifierTypeBlock(entry.Key))
                {
                    ProcessModifiers(entry, scope, sourceId);
                }
            }
        }

        // TODO: remove mandatory sourceId
        public void Process<T, U>(Actions actions, T scope, U sourceId) where T : IEntity<T> where U : IEntityId
        {
            var scopeContext = new ScopeContext { Root = scope, This = scope };
            ProcessActions(actions, scope, scopeContext, sourceId);
		}
	}
}

