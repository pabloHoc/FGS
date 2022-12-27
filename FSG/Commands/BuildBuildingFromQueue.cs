using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct BuildBuildingFromQueue : ICommand
	{
		public string Action { get => "BUILD_BUILDING_FROM_QUEUE"; }

		public EntityId<Land> LandId { get; init; }
	}
}

