using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateAgent: ICommand
	{
		public string Name { get => "CreateAgent"; }

		public string AgentName { get; init; }

		public EntityId<Empire> EmpireId { get; init; }

		public EntityId<Region> RegionId { get; init; }

		public CreateAgent(dynamic payload)
		{
            AgentName = ((Agent)payload).Name;
			EmpireId = ((Agent)payload).EmpireId;
			RegionId = ((Agent)payload).RegionId;
		}
	}
}

