using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct AddBuildingtoQueue : ICommand
	{
		public string Action { get => "ADD_BUILDING_QUEUE"; }

		public string BuildingName { get; init; }

		public EntityId<Land> LandId { get; init; }

		public EntityId<Empire> EmpireId { get; init; }
	}
}

