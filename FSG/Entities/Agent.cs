using System.Collections.Generic;

namespace FSG.Entities
{
    public class Agent : IEntity<Agent>
    {
        public string Type { get => "AGENT"; }

        public EntityId<Agent> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Empire> EmpireId { get; init; }

        public EntityId<Region> RegionId { get; init; }

        public List<Modifier> Modifiers { get; init; }

        public Queue<ActionQueueItem> Actions { get; init; }
    }
}