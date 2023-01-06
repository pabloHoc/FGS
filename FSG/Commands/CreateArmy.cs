using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateArmy: ICommand
	{
		public string Name { get => "CreateArmy"; }

		public int Size { get; init; }

		public int Attack { get; init; }

		public int Defense { get; init; }

		public EntityId<Empire> EmpireId { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

