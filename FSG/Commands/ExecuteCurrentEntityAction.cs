using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct ExecuteCurrentEntityAction<T> : ICommand where T : IEntity<T>, IActor
	{
		public string Name { get => "ExecuteCurrentEntityAction"; }

		public EntityId<T> EntityId { get; init; }
	}
}

