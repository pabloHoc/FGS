using System;
using System.Collections.Generic;

namespace FSG.Entities
{
    public class Agent : IEntity<Agent>, ILocatableEntity, IOwneableEntity, INameableEntity
    {
        public EntityType Type { get; } = EntityType.Agent;

        public EntityId<Agent> Id { get; init; }

        public string Name { get; init; }

        public Nullable<EntityId<Empire>> EmpireId { get; set; }

        public EntityId<Region> RegionId { get; set; }

        public List<Modifier> Modifiers { get; init; }

        public Queue<ActionQueueItem> Actions { get; init; }
    }
}