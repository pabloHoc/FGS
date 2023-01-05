using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Conditions
{
	public class IsLandType : ICondition<Land>
	{
		private readonly string _type;

		public IsLandType(string type)
		{
			_type = type;
		}

		public bool IsValid(GameState gameState, Land land)
        {
			return land.Name == _type;
		}
    }
}

