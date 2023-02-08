using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct DeleteModifier : ICommand
	{
		public string Name => "DeleteModifier";

		public string ModifierName { get; init; }

		public ModifierType ModifierType { get; init; }

		public decimal Value { get; init; }

		public IEntityId TargetId { get; init; }

		public EntityType TargetType { get; init; }

		public IEntityId SourceId { get; init; }
	}
}

