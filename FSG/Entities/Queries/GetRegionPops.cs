using System;
using FSG.Core;

namespace FSG.Entities.Queries
{
	public class GetRegionPops: IQuery<Pop>
	{
		private readonly EntityId<Region> _regionId;

        public GetRegionPops(EntityId<Region> regionId)
		{
            _regionId = regionId;
		}

		public Predicate<Pop> GetPredicate()
		{
			return (pop => pop.RegionId.Equals(_regionId));
		}
	}
}

