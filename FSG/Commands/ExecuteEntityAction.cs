using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct ExecuteEntityAction<T> : ICommand where T : IEntity<T>, IActor
	{
		public string Name { get => "ExecuteEntityAction"; }

		public string ActionName { get; init; }

		public ActionType ActionType { get; init; }

		public EntityId<T> EntityId { get; init; }

		public string Payload { get; init; }
	}
}

