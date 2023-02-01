using System;
using System.Collections.Generic;

namespace FSG.Entities
{
	public class World
	{
		public List<Player> Players { get; init; } = new();

		public List<Empire> Empires { get; init; } = new();

        public List<Region> Regions { get; init; } = new();

        public List<Land> Lands { get; init; } = new();

        public List<Agent> Agents { get; init; } = new();

        public List<Spell> Spells{ get; init; } = new();

        public List<Modifier> Modifiers { get; init; } = new();

        public IEntityId LastAddedEntityId { get; set; }
    }
}

