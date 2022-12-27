using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateAgent: ICommand
	{
		public string Action { get => "CREATE_AGENT"; }

		public string Name { get; init; }

		public EntityId<Empire> EmpireId { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

