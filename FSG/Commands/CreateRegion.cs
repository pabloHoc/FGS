
using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateRegion : ICommand
	{
		public string Action { get => "CREATE_REGION"; }

		public string Name { get; init; }

		public EntityId<Empire>? EmpireId { get; init; }

		public int X { get; init; }

		public int Y { get; init; }
	}
}

