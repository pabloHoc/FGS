using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FSG.Entities
{
    public class Region : IEntity<Region>, IOwneable, INameable
    {
        public EntityType Type => EntityType.Region;

        public EntityId<Region> Id { get; init; }

        public string Name { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public EntityId<Empire> EmpireId { get; set; }

        public int X { get; init; } // TODO: Change to a struct with { x, y } (Coords, Location) 

        public int Y { get; init; }

        public List<EntityId<Region>> ConnectedTo { get; init; }

        public Capital Capital { get; init; }

        public Queue<BuildingQueueItem> BuildingQueue { get; init; }
    }
}