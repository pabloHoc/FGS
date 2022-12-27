using System;
using FSG.Entities;

namespace FSG.Commands
{
	public class SetOwner<T>: ICommand where T : IEntity<T>, IEntityWithOwner
	{
		public string Action { get => "SET_OWNER_EMPIRE"; }

		public string EntityType { get; init; }

		public EntityId<T> TargetId { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

