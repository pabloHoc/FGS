using System;
using FSG.Entities;

namespace FSG.Commands
{
	public class SetLocation<T>: ICommand where T : IEntity<T>, ILocatableEntity
	{
		public string Action { get => "SET_LOCATION"; }

		public EntityType EntityType { get; init; }

		public EntityId<T> EntityId { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

