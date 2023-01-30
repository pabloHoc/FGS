using System;
using System.Collections.Generic;

namespace FSG.Entities
{
    public class Agent : IEntity<Agent>, ILocatable, IOwneable, INameable, IActor
    {
        public EntityType EntityType => EntityType.Agent;

        public EntityId<Agent> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Empire> EmpireId { get; set; }

        public EntityId<Region> RegionId { get; set; }

        public Queue<ActionQueueItem> Actions { get; init; }
    }
}