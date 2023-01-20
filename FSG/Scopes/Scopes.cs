using System;
using System.Security.Principal;
using FSG.Core;
using FSG.Entities;

namespace FSG.Scopes
{
	// TODO: change name
	public class Scopes
	{
		private readonly AgentScope _agentScope;

        public Scopes(GameState gameState)
		{
			_agentScope = new AgentScope(gameState);
        }

		public IBaseEntity GetFrom(Scope scope, IBaseEntity from)
		{
			var entity = from;
			switch(from.Type)
			{
				case EntityType.Agent:
					entity = _agentScope.GetFrom((Agent)entity, scope);
					break;
			}
			return entity;
		}
	}
}

