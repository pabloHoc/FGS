using System.Collections.Generic;

namespace FSG.Entities
{
    public class Land : IEntity<Land>, INameable
    {
        public EntityType EntityType => EntityType.Land;

        public EntityId<Land> Id { get; init; }

        public string Name { get; init; }

        public List<string> Buildings { get; init; }

        public Region Region { get; init; }
    }
}