using System.Collections.Generic;
using FSG.Definitions;
using FSG.Serialization;

namespace FSG.Entities
{
    public class Empire : IEntity<Empire>, INameable
    {
        public EntityType EntityType => EntityType.Empire;

        public EntityId<Empire> Id { get; init; }

        public string Name { get; init; }

        public ResourceBlock Resources { get; init; }

        public string SocialStructure { get; init; }

        public List<Region> Regions { get; init; }
    }
}