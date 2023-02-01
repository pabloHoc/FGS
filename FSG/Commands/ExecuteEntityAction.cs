using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct ExecuteEntityAction : ICommand
	{
		public string Name { get => "ExecuteEntityAction"; }

		public string ActionName { get; init; }

		public ActionType ActionType { get; init; }

		public IEntityId EntityId { get; init; }

		public EntityType EntityType { get; init; }

		public string Payload { get; init; }
	}
}

