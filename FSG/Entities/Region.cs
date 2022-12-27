using System.Collections.Generic;

namespace FSG.Entities
{
    public class Region : IEntity<Region>, IEntityWithOwner
    {
        public EntityType Type { get; } = EntityType.Region;

        public EntityId<Region> Id { get; init; }

        public string Name { get; init; }

        public EntityId<Empire>? EmpireId { get; set; }

        public int X { get; init; } // TODO: Change to a struct with { x, y } (Coords, Location) 

        public int Y { get; init; }

        public List<EntityId<Region>> ConnectedTo { get; init; } 
    }
}