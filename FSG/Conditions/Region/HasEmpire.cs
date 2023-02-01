using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Conditions
{
	public class HasEmpire : ICondition
	{
		private readonly bool _value;

		public HasEmpire(bool value)
		{
			_value = value;
		}

		public bool IsValid(GameState gameState, IBaseEntity region)
        {
			return (((Region)region).Empire != null) == _value;
		}
    }
}

