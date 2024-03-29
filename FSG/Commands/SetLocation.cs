﻿using System;
using FSG.Entities;

namespace FSG.Commands
{
	public struct SetLocation : ICommand
	{
		public string Name { get => "SetLocation"; }

		public EntityType EntityType { get; init; }

		public IEntityId EntityId { get; init; }

		public EntityId<Region> RegionId { get; init; }
	}
}

