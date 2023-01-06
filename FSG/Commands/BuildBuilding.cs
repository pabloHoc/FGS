using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct BuildBuilding : ICommand
	{
		public string Name { get => "BuildBuilding"; }

		public string BuildingName { get; init; }

		public EntityId<Land> LandId { get; init; }
	}
}

