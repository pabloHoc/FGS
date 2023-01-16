using System;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands
{
	public struct BuildBuilding : ICommand
	{
		public string Name { get => "BuildBuilding"; }

		public string BuildingName { get; init; }

		public BuildingType BuildingType { get; init; }

		public EntityId<Land> LandId { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

