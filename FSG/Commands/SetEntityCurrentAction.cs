using System;
using FSG.Entities;

namespace FSG.Commands
{
	public class SetEntityCurrentAction<T>: ICommand where T : IEntity<T>, IActorEntity
	{
		public string Name { get => "SetEntityCurrentAction"; }

		public EntityType EntityType { get; init; }

		public EntityId<T> EntityId { get; init; }

		public ActionQueueItem NewCurrentAction { get; init; }
	}
}

