using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateLand : ICommand
	{
		public string Name { get => "CreateLand"; }

		public string LandName { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

