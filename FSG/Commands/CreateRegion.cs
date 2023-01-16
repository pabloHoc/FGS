
using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateRegion : ICommand
	{
		public string Name { get => "CreateRegion"; }

		public string RegionName { get; init; }

		public EntityId<Empire> EmpireId { get; set; }

		public int X { get; init; }

		public int Y { get; init; }
	}
}

