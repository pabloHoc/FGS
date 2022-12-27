using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateLand : ICommand
	{
		public string Action { get => "CREATE_LAND"; }

		public string Name { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

