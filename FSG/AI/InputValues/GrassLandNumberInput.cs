using System;
using FSG.Core;
using FSG.Entities;
using FSG.Entities.Queries;

namespace FSG.AI
{
	public class GrasslandNumberInput : IInputValue
	{
        private readonly GameState _context;

		public GrasslandNumberInput(GameState context)
		{
			_context = context;
		}

        public int GetValue(IBaseEntity region)
		{
			return ((Region)region).Lands.FindAll(land => land.Name == "Grassland").Count;
		}
	}
}

