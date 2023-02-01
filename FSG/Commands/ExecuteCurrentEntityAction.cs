using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct ExecuteCurrentEntityAction : ICommand
	{
		public string Name { get => "ExecuteCurrentEntityAction"; }

		public IEntityId EntityId { get; init; }

		public EntityType EntityType { get; init; }
	}
}

