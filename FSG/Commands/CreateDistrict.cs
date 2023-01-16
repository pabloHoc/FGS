
using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateDistrict: ICommand
	{
		public string Name { get => "CreateDistrict"; }

		public string DistrictName { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

