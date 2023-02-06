using System;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands
{
	public struct QueueBuilding : ICommand
	{
		public string Name { get => "QueueBuilding"; }

		public BuildingType BuildingType { get; init; }

		public string BuildingName { get; init; }

		public EntityId<Land> LandId { get; init; }

		public EntityId<Region> RegionId { get; init; }

		public EntityId<Empire> EmpireId { get; init; }
	}
}

