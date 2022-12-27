using System;
using FSG.Entities;

namespace FSG.Commands
{
	public class SetOwnerEmpire<T>: ICommand where T : IEntity<T>, IEntityWithOwner
	{
		public string Action { get => "SET_OWNER_EMPIRE"; }

		public EntityType EntityType { get; init; }

		public EntityId<T> EntityId { get; init; }

		public EntityId<Empire> EmpireId { get; init; }
	}
}

