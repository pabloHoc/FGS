using System.Collections.Generic;

namespace FSG.Entities
{
    public class Agent : IEntity<Agent>, IEntityWithLocation, IEntityWithOwner
    {
        public EntityType Type { get; } = EntityType.Agent;

        public EntityId<Agent> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Empire>? EmpireId { get; set; }

        public EntityId<Region> RegionId { get; set; }

        public List<Modifier> Modifiers { get; init; }

        public Queue<ActionQueueItem> Actions { get; init; }
    }
}