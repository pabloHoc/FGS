using System;
using System.Collections.Generic;
using FSG.Commands;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.UtilityAI;

namespace FSG.AI.Operations
{
    // TODO: Could this be defined in a json?
    public class SendAgentToConquestRegion : Operation<GameState, IBaseEntity>
    {
        public SendAgentToConquestRegion(ServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Execute(GameState context, IBaseEntity target)
        {
            var agent = _serviceProvider.GlobalState.World.Agents
                .Find(agent => agent.Empire == ((Player)target).Empire);
            var regionToConquer = agent.Region.ConnectedTo.Find(region => region.Empire == null);
            var conquestActionDefinition = _serviceProvider.Definitions.Get<AgentActionDefinition>("ConquestRegion");

            if (agent.Actions.Count == 0 && regionToConquer != null)
            {
                _serviceProvider.Dispatcher.Dispatch(new ICommand[]
                {
                    new MoveEntity
                    {
                        EntityId = agent.Id,
                        EntityType = agent.EntityType,
                        RegionId = regionToConquer.Id
                    },
                    new QueueEntityAction
                    {
                        ActionName = conquestActionDefinition.Name,
                        SourceEntityId = agent.Id,
                        SourceEntityType = agent.EntityType,
                        Payload = agent.Region.Id,
                        RemainingTurns = conquestActionDefinition.BaseExecutionTime
                    }
                });
            }
        }
    }
}

