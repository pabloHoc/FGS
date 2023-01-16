using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct BuildBuildingFromQueue : ICommand
	{
		public string Name { get => "BuildBuildingFromQueue"; }

		public EntityId<Region> RegionId { get; init; }
	}
}

