using System;

namespace FSG.Entities
{
    public class Army : IEntity<Army>, ILocatable, IOwneable
    {
        public EntityType EntityType => EntityType.Army;

        public EntityId<Army> Id { get; init; }

        public int Size { get; init; }

        public int Attack { get; init; }

        public int Defense { get; init; }

        public Empire Empire { get; set; }

        public Region Region { get; set; }
    }
}