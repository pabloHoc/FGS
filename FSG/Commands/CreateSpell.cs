using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateSpell : ICommand
	{
		public string Name { get => "CreateSpell"; }

		public string SpellName { get; init; }

		public IEntityId TargetId { get; init; }

		public EntityType TargetType { get; init; }

		public int Duration { get; init; }
	}
}

