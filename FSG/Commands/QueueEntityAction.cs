using System;
using FSG.Definitions;
using FSG.Entities;

namespace FSG.Commands
{
	public struct QueueEntityAction : ICommand
	{
		public string Name { get => "QueueEntityAction"; }

		public IEntityId SourceEntityId { get; init; }

		public EntityType SourceEntityType { get; init; }

		public string ActionName { get; init; }

        public string Payload { get; init; }

        public int RemainingTurns { get; set; }
	}
}

