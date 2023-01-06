using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Conditions
{
	public class IsLandType : ICondition
	{
		private readonly string _type;

		public IsLandType(string type)
		{
			_type = type;
		}

		public bool IsValid(GameState gameState, IBaseEntity land)
        {
			return ((Land)land).Name == _type;
		}
    }
}

