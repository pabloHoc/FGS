using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct MoveEntity : ICommand
	{
		public string Name { get => "MoveEntity"; }

		public IEntityId EntityId { get; init; }

		public EntityType EntityType { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

