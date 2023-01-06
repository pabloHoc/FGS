using System;
using FSG.Entities;

namespace FSG.Commands
{
	// TODO: maybe this can be turn into struct if we have a EntityType : typeof(Entity) map

	public class SetLocation<T>: ICommand where T : IEntity<T>, ILocatableEntity
	{
		public string Name { get => "SetLocation"; }

		public EntityType EntityType { get; init; }

		public EntityId<T> EntityId { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

