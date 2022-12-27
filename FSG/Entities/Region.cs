using System.Collections.Generic;

namespace FSG.Entities
{
    public class Region : IEntity<Region>
    {
        public string Type { get => "REGION"; }

        public EntityId<Region> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Empire> EmpireId { get; init; }

        public int X { get; init; }

        public int Y { get; init; }

        public List<EntityId<Region>> ConnectedTo { get; init; } 
    }
}