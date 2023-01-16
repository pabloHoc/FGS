using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Scopes
{
    public class AgentScope
    {
        private readonly GameState _gameState;

        public AgentScope(GameState gameState)
        {
            _gameState = gameState;
        }

        public IBaseEntity GetFrom(Agent agent, Scope scope)
        {
            IBaseEntity entity = agent;
            switch (scope)
            {
                case Scope.Region:
                    entity = GetRegion(agent);
                    break;
                case Scope.Empire:
                    entity = GetEmpire(agent);
                    break;
            }
            return entity;
        }


        private Region GetRegion(Agent agent)
        {
            return _gameState.Entities.Get(agent.RegionId);
        }

        private Empire GetEmpire(Agent agent)
        {
            if (agent.EmpireId != null)
            {
                return _gameState.Entities.Get(agent.EmpireId);
            }
            return null;
        }
    }
}

