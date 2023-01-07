using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct CreateModifier : ICommand
	{
		public string Name { get => "CreateModifier"; }

		public string ModifierName { get; init; }

		public ModifierType ModifierType { get; init; }

		public int Value { get; init; }

		public IEntityId TargetId { get; init; }

		public IEntityId SourceId { get; init; }

		public int Duration { get; init; }
	}
}

