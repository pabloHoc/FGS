using System;
using FSG.Core;

namespace FSG.Entities.Queries
{
	public class GetRegionLands : IQuery<Land>
	{
		private readonly EntityId<Region> _regionId;

        public GetRegionLands(EntityId<Region> regionId)
		{
            _regionId = regionId;
		}

		public Predicate<Land> GetPredicate()
		{
			return (land => land.RegionId.Equals(_regionId));
		}
	}
}

