using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct SetEntityCurrentAction : ICommand
	{
		public string Name { get => "SetEntityCurrentAction"; }

		public EntityType EntityType { get; init; }

		public IEntityId EntityId { get; init; }

		public ActionQueueItem NewCurrentAction { get; init; }
	}
}

