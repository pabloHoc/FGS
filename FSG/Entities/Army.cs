using System;

namespace FSG.Entities
{
    public class Army : IEntity<Army>, ILocatableEntity, IOwneableEntity
    {
        public EntityType Type { get; } = EntityType.Army;

        public EntityId<Army> Id { get; init; }

        public int Size { get; init; }

        public int Attack { get; init; }

        public int Defense { get; init; }

        public Nullable<EntityId<Empire>> EmpireId { get; set; }

        public EntityId<Region> RegionId { get; set; }
    }
}