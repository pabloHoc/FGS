using System;
using FSG.Core;
using FSG.Entities;
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

        private void ProcessActions<T>(Actions actions, T scope, ScopeContext scopeContext) where T : IBaseEntity
        {
			scopeContext.This = scope;
            Scope scopeKey;

            foreach (var entry in actions)
            {
                if (_commandMap.Has<T>(entry.Key))
                {
                    var payload = entry.Value;
                    var scopeContextPayload = scopeContext.GetType().GetProperty((string)payload).GetValue(scopeContext);

                    // Try to get ScopeContext value and send it as command payload 
                    if (scopeContextPayload != null)
                    {
                        payload = scopeContextPayload;
                    }

                    var command = _commandMap.Get(entry.Key, scope, payload);

                    _serviceProvider.Dispatcher.Dispatch(command);
                }
                else if (Enum.TryParse(entry.Key, out scopeKey))
                {
                    var newScope = _serviceProvider.Scopes.GetFrom(scopeKey, scope);
                    scopeContext.Prev = scope;
                    ProcessActions((Actions)entry.Value, newScope, scopeContext);
                }
            }
        }

        public void Process<T>(Actions actions, T scope) where T : IEntity<T>
		{
            ProcessActions<T>(actions, scope, new ScopeContext
            {
                Root = scope,
                This = scope
            });
		}
	}
}

