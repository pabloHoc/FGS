using System;
using FSG.Entities;

namespace FSG.Commands
{
	public class SetOwnerEmpire<T> : ICommand where T : IEntity<T>, IOwneableEntity
	{
		public string Name { get => "SetOwnerEmpire"; }

		public EntityType EntityType { get; init; }

		public EntityId<T> EntityId { get; init; }

		public EntityId<Empire> EmpireId { get; init; }

		public SetOwnerEmpire(T entity, dynamic payload) {
			EntityType = entity.Type;
			EntityId = entity.Id;
			EmpireId = payload.EmpireId;
		}
	}
}

