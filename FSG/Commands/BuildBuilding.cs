using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct BuildBuilding : ICommand
	{
		public string Action { get => "BUILD_BUILDING"; }

		public string BuildingName { get; init; }

		public EntityId<Land> LandId { get; init; }
	}
}

