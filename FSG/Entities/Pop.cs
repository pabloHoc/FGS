using System;

namespace FSG.Entities
{
    // TODO: are pops an entity? -> probably not

	public class Pop : IEntity<Pop>
	{
        public EntityType EntityType => EntityType.Pop;

        public EntityId<Pop> Id { get; init; }

        public Region Region { get; init; }

        public string Strata { get; init; }

        public int Size { get; set; }

        public int GrowthPoints { get; set; }
    }
}

