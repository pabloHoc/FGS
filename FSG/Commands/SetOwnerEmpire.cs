using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct SetOwnerEmpire : ICommand
	{
		public string Name { get => "SetOwnerEmpire"; }

		public EntityType EntityType { get; init; }

		public IEntityId EntityId { get; init; }

		public EntityId<Empire> EmpireId { get; init; }

		public SetOwnerEmpire(dynamic entity, dynamic payload) {
			EntityType = entity.EntityType;
			EntityId = entity.Id;
			EmpireId = payload.EmpireId;
		}
	}
}

