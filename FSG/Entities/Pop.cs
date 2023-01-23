using System;

namespace FSG.Entities
{
    // TODO: are pops an entity? -> probably not

	public class Pop : IEntity<Pop>
	{
        public EntityType Type => EntityType.Pop;

        public EntityId<Pop> Id { get; init; }

        public EntityId<Region> RegionId { get; init; }

        public string Strata { get; init; }

        public int Size { get; init; }
    }
}

