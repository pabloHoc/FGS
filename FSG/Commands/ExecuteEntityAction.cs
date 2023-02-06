using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct ExecuteEntityAction : ICommand
	{
		public string Name { get => "ExecuteEntityAction"; }

		public string ActionName { get; init; }

		public ActionType ActionType { get; init; }

		public IEntityId SourceEntityId { get; init; }

		public EntityType SourceEntityType { get; init; }

		public string Payload { get; init; }
	}
}

