using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct AddBuildingtoQueue : ICommand
	{
		public string Name { get => "AddBuildingtoQueue"; }

		public string BuildingName { get; init; }

		public EntityId<Land> LandId { get; init; }

		public EntityId<Empire> EmpireId { get; init; }
	}
}

